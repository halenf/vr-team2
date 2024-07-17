using FishingGame.FishingRod;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static FishingGame.AI.AgentManager;

namespace FishingGame
{
    namespace AI
    {
        public class AgentManager : MonoBehaviour
        {
            [System.Serializable]
            public class AgentTracker
            {
                public AgentTracker(Agent agent, float lifeTime)
                {
                    m_agent = agent;
                    m_lifeTime = lifeTime;
                }
                
                private Agent m_agent;
                public Agent agent { get { return m_agent; } }

                private float m_lifeTime;
                public float lifeTime { get { return m_lifeTime; } }

                public void ChangeLifeTime(float value)
                {
                    m_lifeTime += value;
                }
            }

            // references
            private Bobber m_bobber;
            private Transform m_playerTransform;

            [SerializeField] private int m_fishToPrespawn = 0;

            [Header("Agent Spawning")]
            [Tooltip("Fish prefab goes here")]
            [SerializeField] private Agent m_agentPrefab;
            //[SerializeField] private GameObject m_fishSilhouettePrefab;
            [Tooltip("Create an empty GameObject for the fish to spawn under.")]
            [SerializeField] private Transform m_agentContainer;

            [Header("Fish Data (Please do not touch, right click component header to load FishData)")]
            [SerializeField] private string m_fishDataPath;
            [SerializeField][NonReorderable] private FishData[] m_fishData;

            [Header("Spawn Timer")]
            [SerializeField] private float m_minSpawnTime;
            [SerializeField] private float m_maxSpawnTime;
            private float m_spawnTimer;
            
            private List<AgentTracker> m_agents;
            public List<Agent> currentAgents { get { return m_agents.Select(agentTracker => agentTracker.agent).ToList(); } }

            private Vector3 m_nextSpawnPosition = Vector3.up;

            private void Start()
            {
                // don't allow the editor to run without any FishData
                if (m_fishData.Length == 0)
                {
                    throw new System.Exception($"The {name} AgentManager in this scene needs FishData to initialise Agents with.");
                }

                // setup agent list
                m_agents = new List<AgentTracker>();

                // set an initial spawn time
                m_spawnTimer = GetRandomSpawnTime();

                // get an initial spawn position
                m_nextSpawnPosition = GameSettings.get_random_position_in_pool();

                // get bobber and player transform
                m_bobber = FindObjectOfType<Bobber>();
                m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

                // pre-spawn some fish
                for (int s = 0; s < m_fishToPrespawn && s < GameSettings.MAX_FISH_COUNT; s++)
                {
                    SpawnAgent();
                    m_nextSpawnPosition = GameSettings.get_random_position_in_pool();
                }
            }

            private void Update()
            {
                // Check if agents need to be despawned
                List<AgentTracker> agentsToDespawn = new List<AgentTracker>();
                foreach (AgentTracker agentTracker in m_agents)
                {
                    if (agentTracker.lifeTime <= 0 || agentTracker.agent.isHooked)
                        agentsToDespawn.Add(agentTracker);
                    else
                        agentTracker.ChangeLifeTime(-Time.deltaTime);
                }

                // despawn agents
                foreach (AgentTracker agentTracker in agentsToDespawn)
                    DespawnAgent(agentTracker);

                // check if agent should spawn
                if (m_spawnTimer <= 0)
                {
                    // always reset spawn timer, even if fish can't spawn
                    // it may look unnatural if whenever a fish disappears, a new one appears instantly
                    m_spawnTimer = GetRandomSpawnTime();

                    if (m_agents.Count < GameSettings.MAX_FISH_COUNT)
                        SpawnAgent();
                }
                else
                    m_spawnTimer -= Time.deltaTime;
            }

            private float GetRandomSpawnTime()
            {
                return Random.Range(m_minSpawnTime, m_maxSpawnTime);
            }

            private void SpawnAgent()
            {
                // Create the agent object
                Agent agent = Instantiate(m_agentPrefab, m_agentContainer);

                // put the agent in a random position in the pool
                agent.transform.position = m_nextSpawnPosition;

                // calc next spawn position
                m_nextSpawnPosition = GameSettings.get_random_position_in_pool();

                // if debugging, the agent prefab may already have a Fish component, so check for that
                Fish fish = agent.gameObject.GetComponent<Fish>();
                if (fish == null)
                {
                    // add the fish component to the agent
                    fish = agent.gameObject.AddComponent<Fish>();

                    // make it a random fish
                    fish.Init(m_fishData[Random.Range(0, m_fishData.Length)]);

                    // add the fish to the agent
                    agent.Init(fish, m_bobber, m_playerTransform);
                }

                // attach the silhouette and scale it
                //GameObject silhouette = Instantiate(m_fishSilhouettePrefab, agent.transform);
                //silhouette.transform.localScale *= (int)fish.data.silhouetteSize + 1;

                // add the agent to the list and set a lifetime for it
                m_agents.Add(new AgentTracker(agent, fish.GetConstraint(fish.data.lifeTime)));
            }

            public void DespawnAgent(AgentTracker agentTracker)
            {
                m_agents.Remove(agentTracker);
                if (!agentTracker.agent.isHooked)
                    Destroy(agentTracker.agent.gameObject);
            }

            public void DespawnAgent(Agent agent)
            {
                m_agents.RemoveAll(agentTracker => agentTracker.agent == agent);
                if (!agent.isHooked)
                    Destroy(agent.gameObject);
            }

            [ContextMenu("Load FishData from Fish Data Path")]
            private void LoadAllFishData()
            {
#if UNITY_EDITOR
                List<FishData> fishData = new List<FishData>();
                string[] guids = AssetDatabase.FindAssets($"t:{typeof(FishData).Name}", new string[] { m_fishDataPath }); //, new string[] { m_fishDataPath }
                foreach (string guid in guids)
                {
                    fishData.Add(AssetDatabase.LoadAssetAtPath<FishData>(AssetDatabase.GUIDToAssetPath(guid)));
                }
                m_fishData = fishData.ToArray();
                if (m_fishData.Length > 0)
                    Debug.Log($"Successfully loaded all FishDatas at {m_fishDataPath} into the {name} Agent Manager.");
#endif
            }

            private void OnDrawGizmos()
            {
#if UNITY_EDITOR
                // draw bobber position
                if (m_bobber != null)
                {
                    Handles.color = Color.red;
                    Handles.DrawWireCube(m_bobber.transform.position, new Vector3(0.5f, 0.5f, 0.5f));
                }

                // draw player
                if (m_playerTransform != null)
                {
                    Handles.color = Color.blue;
                    Handles.DrawWireCube(m_playerTransform.position, new Vector3(0.6f, 2, 0.6f));
                }

                // draw pool bounds
                Handles.color = Color.blue;
                Handles.DrawWireDisc(GameSettings.POOL_ORIGIN, Vector3.up, GameSettings.POOL_RADIUS);

                // draw debug spawn location
                Handles.color = Color.magenta;
                Handles.DrawWireCube(m_nextSpawnPosition, new Vector3(0.5f, 0.5f, 0.5f));
#endif
            }
        }
    }
}

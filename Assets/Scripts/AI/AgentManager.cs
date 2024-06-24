using FishingGame.FishControls;
using FishingGame.Objects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class AgentManager : MonoBehaviour
        {
            public struct AgentTracker
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

            [Header("Constant Objects")]
            private BuoyantObject m_bobberTransform;
            private Transform m_playerTransform;

            [Header("Agent Spawning")]
            [SerializeField] private Agent m_agentPrefab;
            [SerializeField] private GameObject m_fishSilhouettePrefab;
            [SerializeField] private Transform m_agentContainer;

            [Header("Fish Data")]
            [SerializeField] private string m_fishDataPath;
            [SerializeField][NonReorderable] private FishData[] m_fishData;

            [Header("Spawn Timer")]
            [SerializeField] private float m_minSpawnTime;
            [SerializeField] private float m_maxSpawnTime;
            private float m_spawnTimer;
            
            private List<AgentTracker> m_agents;
            public List<Agent> currentAgents { get { return m_agents.Select(agentTracker => agentTracker.agent).ToList(); } }

            private Vector3 m_nextSpawnPosition;

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

                // calc next spawn position
                m_nextSpawnPosition = GameSettings.get_random_position_in_pool();

                // get bobber and player transform
                m_bobberTransform = GameObject.FindGameObjectWithTag("Bobber").GetComponent<BuoyantObject>();
                m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }

            private void Update()
            {
                // Check if agents need to be despawned
                List<AgentTracker> agentsToDespawn = new List<AgentTracker>();
                foreach (AgentTracker agentTracker in m_agents)
                {
                    if (agentTracker.lifeTime <= 0)
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
                    agent.Init(fish, m_bobberTransform, m_playerTransform);
                }

                // attach the silhouette and scale it
                GameObject silhouette = Instantiate(m_fishSilhouettePrefab, agent.transform);
                silhouette.transform.localScale *= (int)fish.data.silhouetteSize + 1;

                // add the agent to the list and set a lifetime for it
                m_agents.Add(new AgentTracker(agent, fish.GetConstraint(fish.data.lifeTime)));

                Debug.Log("New fish spawned!");
            }

            public bool DespawnAgent(AgentTracker agentTracker)
            {
                bool despawned = m_agents.Remove(agentTracker);
                Destroy(agentTracker.agent.gameObject);
                return despawned;
            }

            public bool DespawnAgent(Agent agent)
            {
                bool despawned = m_agents.RemoveAll(agentTracker => agentTracker.agent == agent) > 0;
                Destroy(agent.gameObject);
                return despawned;
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
                    Debug.Log($"Successfully loaded all FishDatas into the {name} Agent Manager.");
#endif
            }

            private void OnDrawGizmos()
            {
                // draw bobber position
                if (m_bobberTransform != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireCube(m_bobberTransform.transform.position, new Vector3(0.5f, 0.5f, 0.5f));
                }

                // draw player
                if (m_playerTransform != null)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawWireCube(m_playerTransform.position, new Vector3(0.6f, 2, 0.6f));
                }

                // draw pool bounds
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(GameSettings.POOL_ORIGIN, GameSettings.POOL_RADIUS);

                // draw debug spawn location
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireCube(m_nextSpawnPosition, new Vector3(0.5f, 0.5f, 0.5f));
            }

            public void ToggleFishParticleDebug()
            {
                FishAnimationController controls = m_agents[0].agent.gameObject.GetComponent<FishAnimationController>();
                
                if (controls != null)
                {
                    if (controls.bubbleSystem.isPlaying)
                        controls.bubbleSystem.Stop();
                    else
                        controls.bubbleSystem.Play();
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using FishingGame.Objects;
using FishingGame.AI;

namespace FishingGame
{

    namespace FishingRod
    {

        public class Bobber : MonoBehaviour
        {
            // Returns true if the bobber is below the water level
            public bool isUnderwater
            {
                get
                {
                    if (m_rodControl.rodState == RodController.RodState.Mounted)
                        return false;
                    return transform.position.y <= GameSettings.POOL_HEIGHT + 0.25f;
                }
            }
            private Agent m_hookedAgent;
            private float m_agentPullStrength;
            // Returns true if there is a hooked agent
            public bool hasHookedAgent { get { return m_hookedAgent != null; } }
            private Transform m_playerTransform;
            // This could be a func, but this works nicely too
            // Returns the distance between the bobber and the player
            private Vector3 playerDistance { get { return transform.position - m_playerTransform.position; } }
            //Returns the direction between the bobber and the player
            private Vector3 rodTipDirection { get { return (transform.position - m_rodControl.getTip.position).normalized; } }
            [Tooltip("The range at which the bobber is pulled out of the water")]
            [SerializeField] private float m_pullRange = 1.0f;
            private RodController m_rodControl;
            private Rigidbody m_rigidbody;
            private BuoyantObject m_datBuoy;
            void Start()
            {
                m_datBuoy = GetComponent<BuoyantObject>();
                m_rigidbody = GetComponent<Rigidbody>();
                m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                m_rodControl = m_playerTransform.GetComponentInChildren<RodController>();
                if (!m_rodControl)
                {
                    Debug.LogError("Bobber cannot find the rod!! Please ensure that the player is tagged correctly <i>(\"Player\")</i> and has a rod controller in one of its children.");
                }
            }
            private void Update()
            {
                // Lock the Y level while underwater
                /*if (isUnderwater)
                    transform.position = new Vector3(transform.position.x, GameSettings.POOL_HEIGHT, transform.position.z);*/

                if(m_rodControl.rodState != RodController.RodState.Mounted)
                ReelIn();
            }

            public void SetAgentPullStrength(float value)
            {
                m_agentPullStrength = value;
            }

            /// <summary>
            /// Pulls the bobber towards or away from the player based on the controller input or hooked fish
            /// </summary>
            public void ReelIn()
            {
                Vector3 delta = Vector3.zero;

                //if there is a hooked agent, pull the bobber out
                if (hasHookedAgent)
                {
                    // get the change in distance that the fish would apply to the bobber
                    delta += rodTipDirection * m_agentPullStrength * Time.deltaTime;
                }

                // get the change in distance that the rod wants to apply to the bobber
                // should be changed to direction to rod tip instead of player position
                delta += rodTipDirection * m_rodControl.getReelForce * Time.deltaTime;

                delta.y = GameSettings.POOL_HEIGHT;
                // apply the change in distance
                // should this be lerped??
                transform.position += new Vector3(delta.x, 0, delta.z);

                // check if the bobber is within range
                if(playerDistance.magnitude <= m_pullRange)
                {
                    //surface the fish if ones hooked, otherwise just mount the bobber
                    if (hasHookedAgent)
                    {
                        m_rodControl.SurfaceFish(m_hookedAgent.fish);
                        Destroy(m_hookedAgent.gameObject);
                    }
                    else
                        m_rodControl.MountBobber();
                }
            }

            /// <summary>
            /// Assigns the fish as the hooked fish
            /// </summary>
            /// <returns>Returns true if the given agent was hooked</returns>
            public bool HookFish(Agent agent)
            {
                if (hasHookedAgent)
                    return false;
                m_hookedAgent = agent;
                m_hookedAgent.transform.SetParent(transform);
                m_hookedAgent.transform.position = transform.position;
                return true;
            }

            private void OnDrawGizmos()
            {
#if UNITY_EDITOR
                if (!hasHookedAgent)
                {
                    Handles.color = Color.yellow;
                    Handles.DrawWireDisc(transform.position, Vector3.up, m_pullRange);
                }
#endif
            }
        }
    }
}

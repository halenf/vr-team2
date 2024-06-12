using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class Agent : MonoBehaviour
        {
            // fish object
            private Fish m_fish;
            public Fish fish { get { return m_fish; } }

            // state machine
            private FiniteStateMachine m_stateMachine;
            public FiniteStateMachine stateMachine { get { return m_stateMachine; } }

            // variables for AI
            [SerializeField] private float m_reachDistance;
            private Vector3 m_targetPosition;
            private GameObject m_target;

            // method variables
            public bool withinRangeOfTarget
            {
                get
                {
                    return Vector2.Distance(transform.position, m_targetPosition) < m_reachDistance;
                }
            }

            public void Start()
            {
                m_fish = GetComponent<Fish>();
                //m_stateMachine = new FiniteStateMachine(new State())
            }

            public void Update()
            {

            }

            public void SetTargetPosition(Vector3 position)
            {
                m_targetPosition = position;
            }
        }
    }
}

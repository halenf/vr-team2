using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class FiniteStateMachine : Behaviour
        {
            [SerializeField] private State[] m_defaultStates;
            
            private State m_currentState;

            private void Awake()
            {
                if (m_defaultStates.Length == 1)
                    m_currentState = Instantiate(m_defaultStates[0], transform);
                else
                    m_currentState = Instantiate(m_defaultStates[Random.Range(0, m_defaultStates.Length)]);
            }

            // base methods
            public override void Enter(Agent agent)
            {
                Debug.Log($"{name} Enter!");
                m_currentState.Enter(agent);
            }
            public override void UpdateThis(Agent agent)
            {
                m_currentState.UpdateThis(agent);
                
                State newState = null;

                foreach (Transition transition in m_currentState.transitions)
                {
                    if (transition.condition.IsTrue(agent))
                    {
                        if (transition.targetStates.Length == 1)
                            newState = transition.targetStates[0];
                        else
                            newState = transition.targetStates[Random.Range(0, transition.targetStates.Length)];
                        break;
                    }
                }

                if (newState != null && newState != m_currentState)
                {
                    Debug.Log($"{name} Entering new state!");
                    m_currentState.Exit(agent);
                    Destroy(m_currentState.gameObject);
                    m_currentState = Instantiate(newState, transform);
                    m_currentState.Enter(agent);
                }
            }
            public override void Exit(Agent agent)
            {
                Debug.Log($"{name} Exit!");
                m_currentState.Exit(agent);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class FiniteStateMachine : Behaviour
        {
            [SerializeField] private State[] m_defaultStates;
            
            private State m_currentState;
            public State currentState { get { return m_currentState; } }

            // base methods
            public override void Enter(Agent agent)
            {
                // if null, then its first state. set it to one of the defaults
                if (m_currentState == null)
                    m_currentState = Instantiate(m_defaultStates[Random.Range(0, m_defaultStates.Length)], transform);
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
                        newState = transition.targetStates[Random.Range(0, transition.targetStates.Length)];
                        break;
                    }
                }

                if (newState != null && newState != m_currentState)
                {
                    m_currentState.Exit(agent);
                    Destroy(m_currentState.gameObject);
                    m_currentState = Instantiate(newState, transform);
                    m_currentState.Enter(agent);
                }
            }
            public override void Exit(Agent agent)
            {
                m_currentState.Exit(agent);
            }
        }
    }
}

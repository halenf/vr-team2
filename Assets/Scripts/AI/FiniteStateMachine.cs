using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        [Serializable]
        public class FiniteStateMachine : Behaviour
        {
            public FiniteStateMachine(State state)
            {
                m_currentState = state;
            }
            
            private State m_currentState;

            // base methods
            public override void Enter(Agent agent)
            {
                m_currentState.Enter(agent);
            }
            public override void UpdateThis(Agent agent)
            {
                State newState = null;

                foreach (Transition transition in m_currentState.transitions)
                {
                    if (transition.condition.IsTrue(agent))
                        newState = transition.targetState;
                }

                if (newState != null && newState != m_currentState)
                {
                    m_currentState.Exit(agent);
                    m_currentState = newState;
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

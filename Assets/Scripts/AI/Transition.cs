using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        [Serializable]
        public class Transition
        {
            public Transition(Condition condition, State targetState)
            {
                m_condition = condition;
                m_targetState = targetState;
            }

            [SerializeReference] private Condition m_condition;
            [SerializeField] private State m_targetState;

            public Condition condition { get { return m_condition; } }
            public State targetState { get { return m_targetState; } }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        [DisallowMultipleComponent]
        public class Transition : MonoBehaviour
        {
            [SerializeField] private Condition m_condition;
            [SerializeField] private State[] m_targetStates;

            public Condition condition { get { return m_condition; } }
            public State[] targetStates { get { return m_targetStates; } }
        }
    }
}

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
            [Tooltip("If any of these are true, the Transition occurs.")]
            [SerializeField] private Condition[] m_conditions;

            [Tooltip("The State this Transition moves to is randomly selected from this array.")]
            [SerializeField] private State[] m_targetStates;

            public Condition[] conditions { get { return m_conditions; } }
            public State[] targetStates { get { return m_targetStates; } }
        }
    }
}

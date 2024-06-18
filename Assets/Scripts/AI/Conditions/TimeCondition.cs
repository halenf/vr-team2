using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class TimeCondition : Condition
        {
            [Tooltip("The amount of time that passes until the Condition passes.")]
            [SerializeField] private float m_time;
            private float m_counter = 0;

            public override bool IsTrue(Agent agent)
            {
                if (m_counter >= m_time)
                {
                    return true;
                }
                return false;
            }

            private void OnEnable()
            {
                m_counter = 0;
            }

            private void Update()
            {
                m_counter += Time.deltaTime;
            }
        }
    }
}

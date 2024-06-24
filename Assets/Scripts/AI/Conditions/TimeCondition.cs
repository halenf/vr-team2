using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class TimeCondition : Condition
        {
            [Tooltip("Whether the Fish will be targeting the Bobber during this State.")]
            [SerializeField] private bool m_targetIsBobber;
            
            [Tooltip("The amount of time that passes until the Condition passes. Leave at 0 to use a value from the Fish.")]
            [SerializeField] private float m_time;

            private float m_counter = 0;

            public override bool IsTrue(Agent agent)
            {
                float time;
                if (m_time == 0)
                    time = m_targetIsBobber ? agent.bobberWaitTime : agent.swimWaitTime;
                else
                    time = m_time;

                if (m_targetIsBobber && !agent.bobberIsUnderwater)
                    return false;

                if (m_counter >= time)
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

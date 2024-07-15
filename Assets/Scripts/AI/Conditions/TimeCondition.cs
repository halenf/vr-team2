using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class TimeCondition : Condition
        {
            [SerializeField] private TargetValueType m_valueType;
            [SerializeField] private float m_time;

            private float m_counter = 0;

            public override void Enter(Agent agent)
            {
                switch (m_valueType)
                {
                    case TargetValueType.FishTarget:
                        m_time = agent.fish.GetConstraint(agent.fish.data.swimWaitTime);
                        break;
                    case TargetValueType.Bobber:
                        m_time = agent.fish.GetConstraint(agent.fish.data.bobberWaitTime);
                        break;
                }
            }

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

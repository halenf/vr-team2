using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public abstract class Condition : ScriptableObject
        {
            public abstract bool IsTrue(Agent agent);
        }

        public class TimeCondition : Condition
        {
            [SerializeField] private float m_time;
            private float m_counter = 0;

            public TimeCondition() { }
            public TimeCondition(float time)
            {
                m_time = time;
                m_counter = 0;
            }

            public override bool IsTrue(Agent agent)
            {
                if (m_counter >= m_time)
                {
                    m_counter = 0;
                    return true;
                }
                else
                {
                    m_counter += Time.deltaTime;
                    return false;
                }
            }
        }

        public class DistanceCondition : Condition
        {
            private float m_distance;
            private bool m_lessThan;

            public DistanceCondition() { }
            public DistanceCondition(float distance, bool lessThan = true)
            {
                m_distance = distance;
                m_lessThan = lessThan;
            }

            public override bool IsTrue(Agent agent)
            {
                return agent.withinRangeOfTarget == m_lessThan;
            }
        }
    }
}
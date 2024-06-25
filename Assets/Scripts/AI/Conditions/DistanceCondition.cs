using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class DistanceCondition : Condition
        {
            [SerializeField] private TargetType m_targetType = 0;

            [SerializeField] private float m_distance;
            [SerializeField] private Transform m_target;

            [Tooltip("If this Condition passes when the target is within the range of Distance or outside of it.")]
            [SerializeField] private bool m_withinRange;

            public override void Enter(Agent agent)
            {
                switch (m_targetType)
                {
                    case TargetType.Target:
                        m_distance = agent.fish.GetConstraint(agent.fish.data.swimDetectionRange);
                        break;
                    case TargetType.Bobber:
                        m_distance = agent.fish.GetConstraint(agent.fish.data.bobberDetectionRange);
                        break;
                }
            }

            public override bool IsTrue(Agent agent)
            {
                // decide on target
                Vector3 targetPosition = Vector3.zero;
                switch (m_targetType)
                {
                    case TargetType.Value:
                        targetPosition = m_target.position;
                        break;
                    case TargetType.Target:
                        targetPosition = agent.targetPosition;
                        break;
                    case TargetType.Bobber:
                        if (!agent.bobberIsUnderwater == m_withinRange)
                            return false;
                        targetPosition = agent.bobberPosition;
                        break;
                }

                return Vector3.Distance(agent.transform.position, targetPosition) <= m_distance == m_withinRange;
            }

            private void OnDrawGizmos()
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, m_distance);
            }
        }
    }
}
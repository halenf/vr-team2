using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class DistanceCondition : Condition
        {
            [Tooltip("false: compares the distance with the Agent's target Position. true: compares the distance with the Bobber's position.")]
            [SerializeField] private bool m_targetIsBobber;

            [Tooltip("The distance used to check if the condition passes. Leave at 0 to use a value from the Fish.")]
            [SerializeField] private float m_distance;

            private float m_gizmoRadius;

            [Tooltip("If this Condition passes when the target is within the distance or outside of it.")]
            [SerializeField] private bool m_withinRange;

            public override bool IsTrue(Agent agent)
            {
                // decide if should used provided value or value from fish
                float distance;
                if (m_distance == 0)
                    distance = m_targetIsBobber ? agent.bobberDetectionRange : agent.swimDetectionRange;
                else
                    distance = m_distance;

                m_gizmoRadius = distance;
                
                // decide on target
                Vector3 targetPosition = m_targetIsBobber ? agent.bobberPosition : agent.targetPosition;

                if (m_targetIsBobber && !agent.bobberIsUnderwater)
                    return false == m_withinRange;

                return Vector3.Distance(agent.transform.position, targetPosition) < distance == m_withinRange;
            }

            private void OnDrawGizmos()
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, m_gizmoRadius);
            }
        }
    }
}
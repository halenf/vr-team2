using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class DistanceCondition : Condition
        {
            [SerializeField] private TargetValueType m_valueType = 0;

            [SerializeField] private float m_distance;
            [SerializeField] private Transform m_target;

            [Tooltip("If this Condition passes when the target is within the range of Distance or outside of it.")]
            [SerializeField] private bool m_withinRange;

            public override void Enter(Agent agent)
            {
                switch (m_valueType)
                {
                    case TargetValueType.FishTarget:
                        m_distance = agent.fish.GetConstraint(agent.fish.data.swimDetectionRange);
                        break;
                    case TargetValueType.Bobber:
                        m_distance = agent.fish.GetConstraint(agent.fish.data.bobberDetectionRange);
                        break;
                    case TargetValueType.Spooked:
                        m_distance = agent.fish.GetConstraint(agent.fish.data.spookRange);
                        break;
                }
            }

            public override bool IsTrue(Agent agent)
            {
                // decide on target
                Vector3 targetPosition = Vector3.zero;
                switch (m_valueType)
                {
                    case TargetValueType.Value:
                        targetPosition = m_target.position;
                        break;
                    case TargetValueType.FishTarget:
                        targetPosition = agent.targetPosition;
                        break;
                    case TargetValueType.Spooked:
                    case TargetValueType.Bobber:
                        if (!agent.bobber.isUnderwater == m_withinRange)
                            return false;
                        targetPosition = agent.bobberPosition;
                        break;
                }

                return Vector3.Distance(agent.transform.position, targetPosition) <= m_distance == m_withinRange;
            }

            private void OnDrawGizmos()
            {
#if UNITY_EDITOR
                switch (m_valueType)
                {
                    case TargetValueType.Value:
                        Handles.color = Color.red;
                        break;
                    case TargetValueType.FishTarget:
                        Handles.color = new Color(0, 0.45f, 0);
                        break;
                    case TargetValueType.Bobber:
                        Handles.color = new Color(0.45f, 0, 0);
                        break;
                    case TargetValueType.Spooked:
                        Handles.color = new Color(0, 0, 0.45f);
                        break;
                }

                Handles.DrawWireDisc(transform.position, Vector3.up, m_distance);
#endif
            }
        }
    }
}
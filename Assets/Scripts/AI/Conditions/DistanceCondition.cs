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
            [Tooltip("false: compares the distance with the Agent's target Position. true: compares the distance with the Bobber's position.")]
            [SerializeField] private bool m_targetIsBobber;

            [Tooltip("The range the agent must be within or outside.")]
            [SerializeField] private float m_distance;

            [Tooltip("If this Condition passes when the target is within the distance or outside of it.")]
            [SerializeField] private bool m_withinRange;

            public override bool IsTrue(Agent agent)
            {
                return Vector3.Distance(agent.transform.position, m_targetIsBobber ? agent.bobberPosition : agent.targetPosition) < m_distance == m_withinRange;
            }

            private void OnDrawGizmos()
            {
                Handles.color = Color.red;
                Handles.DrawWireDisc(transform.position, Vector3.up, m_distance);
            }
        }
    }
}
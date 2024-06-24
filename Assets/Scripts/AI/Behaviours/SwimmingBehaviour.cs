using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class SwimmingBehaviour : Behaviour
        {
            private float m_speed;
            
            public override void Enter(Agent agent)
            {
                // get the random position to add
                float xRange = agent.fish.GetConstraint(agent.fish.data.swimRange);
                float zRange = agent.fish.GetConstraint(agent.fish.data.swimRange);

                // calc the new target position
                float xTarget = agent.transform.position.x + Random.Range(-xRange, xRange);
                float zTarget = agent.transform.position.z + Random.Range(-zRange, zRange);

                agent.SetTargetPosition(Vector3.ClampMagnitude(new Vector3(xTarget, GameSettings.POOL_HEIGHT, zTarget), GameSettings.POOL_RADIUS));

                m_speed = agent.fish.GetConstraint(agent.fish.data.swimSpeed);
            }

            public override void UpdateThis(Agent agent)
            {
                agent.MoveTowardsTarget(m_speed);
                agent.LookAtTarget();
            }
        }
    }
}


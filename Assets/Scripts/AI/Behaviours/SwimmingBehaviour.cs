using System.Collections;
using System.Collections.Generic;
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
                Vector3 targetPosition = new Vector3(xTarget, GameSettings.POOL_HEIGHT, zTarget);
                
                // clamp target position to pool radius
                if (Vector3.Distance(targetPosition, GameSettings.POOL_ORIGIN) > GameSettings.POOL_RADIUS)
                {
                    Vector3 direction = (targetPosition - GameSettings.POOL_ORIGIN).normalized;
                    targetPosition = direction * GameSettings.POOL_RADIUS;
                    targetPosition += GameSettings.POOL_ORIGIN;
                    targetPosition.y = GameSettings.POOL_HEIGHT;
                }

                agent.SetTargetPosition(targetPosition);

                m_speed = agent.fish.GetConstraint(agent.fish.data.swimSpeed);

                agent.animationController.ToggleBubbles(true);
            }

            public override void UpdateThis(Agent agent)
            {
                agent.MoveTowardsTarget(m_speed);
                agent.LookAtTarget();
            }
        }
    }
}


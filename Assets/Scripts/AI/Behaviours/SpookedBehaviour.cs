using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class SpookedBehaviour : Behaviour
        {
            [Tooltip("Degrees of the range of the random angle for the direction the Fish will run away in.")]
            [SerializeField] private float m_randomAngleRange;

            private float m_speed;

            public override void Enter(Agent agent)
            {
                // get the random position to add
                float xRange = agent.fish.GetConstraint(agent.fish.data.spookRange) * 2;
                float zRange = agent.fish.GetConstraint(agent.fish.data.spookRange) * 2;

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

                // calc spook speed
                m_speed = agent.fish.GetConstraint(agent.fish.data.spookSpeed);

                // set the target position of the fish
                agent.SetTargetPosition(targetPosition);

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

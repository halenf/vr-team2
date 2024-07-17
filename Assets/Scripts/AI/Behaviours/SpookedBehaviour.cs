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
                // get the direction of the bobber to the fish
                Vector3 awayFromBobber = (agent.transform.position - agent.bobberPosition).normalized;

                // apply the angle rotation to the direction and set length
                Vector3 targetPosition = awayFromBobber * agent.fish.GetConstraint(agent.fish.data.spookRange) * 2f;
                targetPosition.y = GameSettings.POOL_HEIGHT;

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

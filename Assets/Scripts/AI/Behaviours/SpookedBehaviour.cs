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
                Vector3 oppositePlayerDirection = (agent.bobberPosition - agent.transform.position).normalized;

                // get a random angle within the specified range
                float randomAngle = Random.Range(-m_randomAngleRange / 2f, m_randomAngleRange / 2f);

                // apply the angle rotation to the direction and set length
                Vector3 targetPosition = Quaternion.Euler(0, randomAngle, 0) * oppositePlayerDirection * agent.fish.GetConstraint(agent.fish.data.spookRange);

                // clamp target position to pool radius
                if (Vector3.Distance(targetPosition, GameSettings.POOL_ORIGIN) > GameSettings.POOL_RADIUS)
                {
                    Vector3 direction = (GameSettings.POOL_ORIGIN - targetPosition).normalized;
                    targetPosition = direction * GameSettings.POOL_RADIUS;
                    targetPosition.y = GameSettings.POOL_HEIGHT;
                }

                // set the target position of the fish
                agent.SetTargetPosition(targetPosition);

                agent.animationController.ToggleBubbles(true);
            }

            public override void UpdateThis(Agent agent)
            {
                agent.MoveTowardsTarget(-m_speed);
                agent.LookAwayFromTarget();
            }
        }
    }
}

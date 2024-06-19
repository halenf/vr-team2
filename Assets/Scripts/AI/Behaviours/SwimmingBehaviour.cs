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

                // get the static values
                Vector3 poolOrigin = GameSettings.POOL_ORIGIN;
                Vector2 poolBounds = GameSettings.POOL_BOUNDS;

                // clamp the target inside the pool bounds
                float xTarget = Mathf.Clamp(agent.transform.position.x + Random.Range(-xRange, xRange), poolOrigin.x - poolBounds.x, poolOrigin.x + poolBounds.x);
                float zTarget = Mathf.Clamp(agent.transform.position.z + Random.Range(-zRange, zRange), poolOrigin.z - poolBounds.y, poolOrigin.z + poolBounds.y);

                agent.SetTargetPosition(new Vector3(xTarget, GameSettings.POOL_HEIGHT, zTarget));

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


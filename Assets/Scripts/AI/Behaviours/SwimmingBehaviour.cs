using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class SwimmingBehaviour : Behaviour
        {
            public override void Enter(Agent agent)
            {
                float xRange = agent.fish.RandomConstraint(agent.fish.data.swimRange);
                float zRange = agent.fish.RandomConstraint(agent.fish.data.swimRange);
                agent.SetTargetPosition(new Vector3(agent.transform.position.x + Random.Range(-xRange, xRange),
                    GameSettings.POOL_HEIGHT,
                    agent.transform.position.y + Random.Range(-zRange, zRange)));
            }

            public override void UpdateThis(Agent agent)
            {
                agent.MoveTowardsTarget();
                agent.LookAtTarget();
            }
        }
    }
}


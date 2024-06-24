using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class HookedBehaviour : Behaviour
        {
            public override void Enter(Agent agent)
            {
                agent.SetTargetPosition(agent.playerPosition);
                Vector3 currentPosition = agent.transform.position;
                agent.transform.position = new Vector3(currentPosition.x, GameSettings.POOL_HEIGHT, currentPosition.z);

                agent.animationController.ToggleBubbles(true);
            }

            public override void UpdateThis(Agent agent)
            {
                agent.SetTargetPosition(agent.playerPosition);
                agent.PullBobberAwayFromTarget();
                agent.LookAwayFromTarget();
            }
        }
    }

}

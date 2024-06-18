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
            }

            public override void UpdateThis(Agent agent)
            {
                agent.SetTargetPosition(agent.playerPosition);
                agent.MoveBobberAwayFromPlayer();
                agent.transform.position = agent.bobberPosition;
                agent.LookAwayFromTarget();
            }
        }
    }

}

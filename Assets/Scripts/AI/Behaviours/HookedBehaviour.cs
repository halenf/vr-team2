using FishingGame.FishingRod;
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
                agent.animationController.ToggleBubbles(true);
                agent.SetTargetPosition(agent.playerPosition);
            }
            public override void UpdateThis(Agent agent)
            {
                agent.LookAwayFromTarget();
            }
        }
    }

}

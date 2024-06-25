using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class WaitBobberBehaviour : Behaviour
        {
            public override void Enter(Agent agent)
            {
                agent.SetTargetPosition(agent.bobberPosition);

                agent.animationController.ToggleBubbles(false);
            }

            public override void UpdateThis(Agent agent)
            {
                agent.LookAtTarget();
            }
        }
    }
}

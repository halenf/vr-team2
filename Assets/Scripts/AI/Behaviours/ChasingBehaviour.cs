using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class ChasingBehaviour : Behaviour
        {
            public override void Enter(Agent agent)
            {
                agent.SetTargetPosition(agent.bobberPosition);
            }

            public override void UpdateThis(Agent agent)
            {
                agent.SetTargetPosition(agent.bobberPosition);
                agent.MoveTowardsTarget();
                agent.LookAtTarget();
            }
        }
    }
}

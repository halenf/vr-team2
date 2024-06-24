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
            private float m_speed;

            public override void Enter(Agent agent)
            {
                agent.SetTargetPosition(agent.bobberPosition);
                m_speed = agent.fish.GetConstraint(agent.fish.data.chaseSpeed);
            }

            public override void UpdateThis(Agent agent)
            {
                agent.SetTargetPosition(agent.bobberPosition);
                agent.MoveTowardsTarget(m_speed);
                agent.LookAtTarget();
            }
        }
    }
}

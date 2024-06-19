using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class WaitingBehaviour : Behaviour
        {
            [SerializeField] private bool m_lookAtBobber;

            public override void Enter(Agent agent)
            {
                if (m_lookAtBobber)
                {
                    agent.SetTargetPosition(agent.bobberPosition);
                }
                else
                {
                    Vector2 randomLook = Random.insideUnitCircle;
                    agent.SetTargetPosition(transform.position + new Vector3(randomLook.x, GameSettings.POOL_HEIGHT, randomLook.y));
                }
            }

            public override void UpdateThis(Agent agent)
            {
                agent.LookAtTarget();
            }
        }
    }
}

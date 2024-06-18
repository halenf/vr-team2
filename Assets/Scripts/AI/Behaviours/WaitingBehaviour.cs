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
                Vector2 randomLook = Random.insideUnitCircle;
                agent.SetTargetPosition(m_lookAtBobber ? agent.bobberPosition : transform.position + new Vector3(randomLook.x, 0, randomLook.y));
            }

            public override void UpdateThis(Agent agent)
            {
                agent.LookAtTarget();
            }
        }
    }
}

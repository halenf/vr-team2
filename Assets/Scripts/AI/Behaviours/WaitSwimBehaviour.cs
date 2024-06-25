using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class WaitSwimBehaviour : Behaviour
        {
            private enum LookBehaviour
            {
                None,
                RandomLook,
                AlwaysLook
            }

            [SerializeField] private LookBehaviour m_lookBehaviour = 0;
            
            public override void Enter(Agent agent)
            {
                switch (m_lookBehaviour)
                {
                    case LookBehaviour.RandomLook:
                        // randomly decide if it should look at a random target
                        if (Random.Range(0, 2) == 0)
                            goto case LookBehaviour.AlwaysLook;
                        break;
                    case LookBehaviour.AlwaysLook:
                        Vector2 randomLook = Random.insideUnitCircle;
                        agent.SetTargetPosition(transform.position + new Vector3(randomLook.x, GameSettings.POOL_HEIGHT, randomLook.y));
                        break;
                }
                
                agent.animationController.ToggleBubbles(false);
            }

            public override void UpdateThis(Agent agent)
            {
                agent.LookAtTarget();
            }
        }
    }
}

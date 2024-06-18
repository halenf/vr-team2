using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class SendToPlayerBehaviour : Behaviour
        {
            public override void Enter(Agent agent)
            {
                // send the fish to the rod
                Destroy(gameObject);
            }
        }
    }
}
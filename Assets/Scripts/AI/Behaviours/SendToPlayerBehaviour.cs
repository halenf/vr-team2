using FishingGame.Player;
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
                // send the fish details to the player
                FindObjectOfType<RodController>().MountBobber();

                // despawn the agent
                FindObjectOfType<AgentManager>().DespawnAgent(agent);
            }
        }
    }
}

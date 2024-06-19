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
                //FindObjectOfType<ReelFish>().SurfaceFish(agent.fish);
                
                // get the Agent/Fish object
                Transform parent = transform.parent;
                while (parent.GetComponent<Agent>() == null)
                {
                    parent = parent.parent;
                }

                // destroy it
                Destroy(parent.gameObject);
            }
        }
    }
}

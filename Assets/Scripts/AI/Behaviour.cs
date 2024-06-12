using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public abstract class Behaviour
        {
            public virtual void Enter(Agent agent) { }
            public virtual void Update(Agent agent) { }
            public virtual void Exit(Agent agent) { }
        }

        public class SwimmingBehaviour : Behaviour
        {
            public override void Enter(Agent agent)
            {
                agent.SetTargetPosition(new Vector2(agent.transform.position.x + agent.fish.RandomConstraint(agent.fish.data.swimRange),
                        agent.transform.position.y + agent.fish.RandomConstraint(agent.fish.data.swimRange)));
            }
        }
    }
}

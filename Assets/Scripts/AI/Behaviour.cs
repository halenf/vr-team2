using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        [Serializable]
        public abstract class Behaviour : ScriptableObject
        {
            public virtual void Enter(Agent agent) { }
            public virtual void UpdateThis(Agent agent) { }
            public virtual void Exit(Agent agent) { }
        }

        [Serializable]
        public class SwimmingBehaviour : Behaviour
        {
            public override void Enter(Agent agent)
            {
                agent.SetTargetPosition(new Vector2(agent.transform.position.x + agent.fish.RandomConstraint(agent.fish.data.swimRange),
                        agent.transform.position.y + agent.fish.RandomConstraint(agent.fish.data.swimRange)));
            }
        }

        [Serializable]
        public class ChasingBehaviour : Behaviour
        {

        }
    }
}

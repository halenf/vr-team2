using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class BobberHasFishCondition : Condition
        {
            [Tooltip("Whether the condition should be true when the Bobber has a fish or doesn't.")]
            [SerializeField] private bool m_hasFish;
            
            public override bool IsTrue(Agent agent)
            {
                return agent.bobber.hasHookedAgent == m_hasFish;
            }
        }
    }
}

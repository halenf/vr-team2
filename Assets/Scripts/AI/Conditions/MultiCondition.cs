using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class MultiCondition : Condition
        {
            [SerializeReference] private List<Condition> m_conditions;

            public override void Enter(Agent agent)
            {
                foreach (Condition condition in m_conditions)
                    condition.Enter(agent);
            }

            public override bool IsTrue(Agent agent)
            {
                foreach (Condition condition in m_conditions)
                {
                    if (!condition.IsTrue(agent))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}

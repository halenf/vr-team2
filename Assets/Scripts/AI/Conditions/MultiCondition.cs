using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        [Serializable]
        public class MultiCondition : Condition
        {
            [SerializeReference] private List<Condition> m_conditions;

            public override bool IsTrue(Agent agent)
            {
                return m_conditions.TrueForAll(condition => IsTrue(agent));
            }
        }
    }
}

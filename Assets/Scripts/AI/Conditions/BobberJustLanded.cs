using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class BobberJustLanded : Condition
        {
            private bool m_bobberIsUnderwater = true;

            public override bool IsTrue(Agent agent)
            {
                if (!m_bobberIsUnderwater && agent.bobber.isUnderwater)
                    return true;

                m_bobberIsUnderwater = agent.bobber.isUnderwater;
                return false;
            }
        }
    }
}

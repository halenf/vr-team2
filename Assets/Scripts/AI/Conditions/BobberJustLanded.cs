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
                if (!m_bobberIsUnderwater && agent.bobberIsUnderwater)
                    return true;

                m_bobberIsUnderwater = agent.bobberIsUnderwater;
                return false;
            }
        }
    }
}

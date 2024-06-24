using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishingGame.Objects;
using FishingGame.AI;

namespace FishingGame
{

    namespace FishingRod
    {

        public class Bobber : MonoBehaviour
        {
            private BuoyantObject m_datBuoy;
            public bool isUnderwater { get { return m_datBuoy.isUnderwater; } }
            private Agent m_hookedAgent;
            public bool hasHookedAgent { get { return m_hookedAgent != null; } }

            // Start is called before the first frame update
            void Start()
            {
                m_datBuoy = GetComponent<BuoyantObject>();
            }

            // Update is called once per frame
            void Update()
            {
                
            }

            public void ReelIn()
            {
                Vector3 delta = Vector3.zero;

                if (hasHookedAgent)
                {
                    //delta += m_hookedAgent.pullStrength;
                }

                //delta += rod.reelStrength;

                transform.position += delta;
            }

            public void HookFish(Agent agent)
            {
                m_hookedAgent = agent;
                m_hookedAgent.transform.SetParent(transform, false);
            }
        }
    }
}

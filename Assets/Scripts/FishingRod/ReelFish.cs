using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace FishingGame
{
    namespace Player
    {
        public class ReelFish : MonoBehaviour
        {
            private Fish m_hookedFish;
            public Fish hookFish { get { return m_hookedFish; } set { m_hookedFish = value; } }
            [SerializeField] private Transform m_fishDisplayPoint;
            [SerializeField] private float m_reelSpeed = 1;
            /// <summary>
            /// Upon getting near to the player/pier, pull the fish from the water.
            /// </summary>
            public int SurfaceFish()
            {
                if (m_hookedFish)
                {
                    //Instance the fish model as a child of the empty parent
                    GameObject caughtFish = Instantiate(m_hookedFish.data.fishModel, m_fishDisplayPoint);
                    //Its a grabbale kinematic rigidbody, so add the components
                    Rigidbody fishRb = caughtFish.AddComponent<Rigidbody>();
                    XRGrabInteractable fishGrab = caughtFish.AddComponent<XRGrabInteractable>();
                    fishRb.isKinematic = true;
                    fishGrab.useDynamicAttach = true;
                    return 0;
                }
                return -1;
            }
        }
    }
}

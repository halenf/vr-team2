using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace FishingGame
{
    namespace Player
    {
        public class ReelFish : MonoBehaviour
        {
            private Transform m_player;
            private Transform m_bobber;
            [SerializeField] private float m_catchRadius;
            private Fish m_hookedFish;
            public Fish SetHookedFish { set { m_hookedFish = value; } }
            [SerializeField] private Transform m_fishDisplayPoint;
            
            private void Start()
            {
                m_player = GameObject.FindGameObjectWithTag("Player").transform;
                m_bobber = GameObject.FindGameObjectWithTag("Bobber").transform;
            }
            /// <summary>
            /// Upon getting near to the player/pier, pull the fish from the water.
            /// </summary>
            public int SurfaceFish()
            {
                if (Vector3.Distance(m_player.position, m_bobber.position) < m_catchRadius)
                {
                    //Instance the fish model as a child of the empty parent
                    GameObject caughtFish = Instantiate(m_hookedFish.data.model, m_fishDisplayPoint);
                    //Its a grabbale kinematic rigidbody, so add the components
                    Rigidbody fishRb = caughtFish.AddComponent<Rigidbody>();
                    XRGrabInteractable fishGrab = caughtFish.AddComponent<XRGrabInteractable>();
                    fishRb.isKinematic = true;
                    fishGrab.useDynamicAttach = true;
                    FindObjectOfType<RodController>().MountBobber();
                    return 0;
                }
                return -1;
            }
        }
    }
}

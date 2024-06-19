using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace Player
    {
        public class ReelFish : MonoBehaviour
        {
            private object m_hookedFish;
            public object hookFish { set { m_hookedFish = value; } }
            [SerializeField] private Transform m_fishDisplayPoint;
            [SerializeField] private float m_reelSpeed = 1;
            /// <summary>
            /// On the player unit, pull the fish towards the player.
            /// </summary>
            /// <returns></returns>
            public int ReelInFish()
            {
                if(m_hookedFish != null)
                {
                    //Pull in the hooked fish
                    GameObject fishAI = (GameObject)m_hookedFish;
                    fishAI.transform.position = Vector3.Lerp(new Vector3(fishAI.transform.position.x, 0, fishAI.transform.position.z), new Vector3(transform.position.x, 0, transform.position.z), m_reelSpeed); ;
                    return 0;
                }
                return -1;
            }
            /// <summary>
            /// Upon getting near to the player/pier, pull the fish from the water.
            /// </summary>
            /// <returns></returns>
            public int SurfaceFish()
            {
                //Launch the fish out of the water
                FishData fish = (FishData)m_hookedFish;
                GameObject caughtFish = Instantiate(fish.model);
                //Its a grabbale kinematic rigidbody
                Rigidbody fishRb = caughtFish.AddComponent<Rigidbody>();
                fishRb.isKinematic = true;
                //caughtFish.AddComponent(Grabbable);
                //Display the fish in front of the player
                caughtFish.transform.position = m_fishDisplayPoint.position;
                return 0;
            }
        }
    }
}

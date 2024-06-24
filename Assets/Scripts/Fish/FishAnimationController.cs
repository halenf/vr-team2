using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace FishControls
    {
        public class FishAnimationController : MonoBehaviour
        {
            [Header("Shadow")]
            [SerializeField] private ParticleSystem m_shadowSystem;
            [SerializeField] private float m_minShadowtime;
            [SerializeField] private float m_maxShadowTime;

            [Header("Bubbles")]
            [SerializeField] private ParticleSystem m_bubbleSystem;
            public ParticleSystem bubbleSystem { get { return m_bubbleSystem; } }
            [SerializeField] private float m_minBubbleTime;
            [SerializeField] private float m_maxBubbleTime;

            private void Update()
            {
                
            }

            public void ToggleParticleDebug()
            {
                
            }
        }
    }
}

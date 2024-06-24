using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

namespace FishingGame
{
    namespace FishControls
    {
        public class FishAnimationController : MonoBehaviour
        {
            [Header("Bubbles")]
            [SerializeField] private ParticleSystem m_bubbleSystem;

            private void Update()
            {
                
            }

            public void ToggleBubbles(bool value)
            {
                if (value)
                    m_bubbleSystem.Play();
                else
                    m_bubbleSystem.Stop();
            }
        }
    }
}

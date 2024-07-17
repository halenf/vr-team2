using FishingGame.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace FishControls
    {
        public class FishAnimationController : MonoBehaviour
        {
            [Header("Bubbles")]
            [SerializeField] private ParticleSystem m_bubbleSystem;
            [SerializeField, Range(0f, 1f)] private float m_soundOnMoveFrequency;

            private SFXController m_sfxController;
            public SFXController sfxController { get { return m_sfxController; } }

            private void Start()
            {
                m_sfxController = GetComponent<SFXController>();
            }

            public void ToggleBubbles(bool value)
            {
                if (value == m_bubbleSystem.isPlaying)
                    return;
                
                if (value)
                {
                    m_bubbleSystem.Play();
                    if (m_soundOnMoveFrequency >= 1 || Random.Range(0f, 1f) <= m_soundOnMoveFrequency)
                        m_sfxController.PlayRandomSoundClipFromCollectionAtPosition("Bubble Sounds", transform.position);
                }
                else
                    m_bubbleSystem.Stop();
            }
        }
    }
}

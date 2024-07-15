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

            private SFXController m_sfxController;
            public SFXController sfxController { get { return m_sfxController; } }

            private void Start()
            {
                m_sfxController = GetComponent<SFXController>();
            }

            public void ToggleBubbles(bool value)
            {
                if (value)
                {
                    m_bubbleSystem.Play();
                    m_sfxController.PlayRandomSoundClipFromCollectionAtPosition("Bubble Sounds", transform.position);
                }
                else
                    m_bubbleSystem.Stop();
            }
        }
    }
}

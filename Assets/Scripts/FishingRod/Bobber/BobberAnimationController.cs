using FishingGame.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace FishingRod
    {
        public class BobberAnimationController : MonoBehaviour
        {
            private Bobber m_bobber;
            private SFXController m_sfxController;
            private bool m_isUnderwater = false;

            // particle systems
            [Header("Random Timed Ripples")]
            [Tooltip("The particle effect that plays while the Bobber is in the water.")]
            [SerializeField] private ParticleSystem m_rippleParticle;
            [SerializeField] private float m_minRippleTimer;
            [SerializeField] private float m_maxRippleTimer;
            private float m_timer = 0;

            [Header("Splashes")]
            [Tooltip("The splash for when the Bobber hits or leaves the water.")]
            [SerializeField] private ParticleSystem m_splashParticle;
            [Tooltip("The splash for when the Bobber leaves the water with a Fish hooked.")]
            [SerializeField] private ParticleSystem m_fishPullParticle;

            // Start is called before the first frame update
            void Start()
            {
                m_bobber = GetComponent<Bobber>();
                m_sfxController = GetComponent<SFXController>();
                SetTimerValue();
            }

            // Update is called once per frame
            void Update()
            {
                // When the bobber hits the water
                if (!m_isUnderwater && m_bobber.isUnderwater)
                {
                    m_sfxController.PlayRandomSoundClipFromCollectionAtPosition("Land Splashes", transform.position);
                    Instantiate(m_splashParticle);
                    SetTimerValue();
                }

                // when the bobber leaves the water
                if (m_isUnderwater && !m_bobber.isUnderwater)
                {
                    m_sfxController.PlayRandomSoundClipFromCollectionAtPosition("Leave Splashes", transform.position);
                    Instantiate(m_fishPullParticle);
                }

                // update held value
                m_isUnderwater = m_bobber.isUnderwater;

                // play ripples if the bobber is in the water
                if (m_isUnderwater)
                {
                    if (m_timer <= 0)
                    {
                        Instantiate(m_rippleParticle, transform.position, Quaternion.identity);
                        SetTimerValue();
                    }
                    else
                        m_timer -= Time.deltaTime;
                }
            }

            private void SetTimerValue()
            {
                m_timer = Random.Range(m_minRippleTimer, m_maxRippleTimer);
            }
        }
    }
}

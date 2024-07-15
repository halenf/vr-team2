using FishingGame;
using FishingGame.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FishingGame
{
    public class DestroyObject : MonoBehaviour
    {
        [Tooltip("An array of particles that are randomly chosen on water contact.")]
        [SerializeField] private ParticleSystem[] m_particles;
        [Tooltip("The delay for destruction.")]
        [SerializeField, Min(0)] private float m_timer;

        private bool m_splashed = false;

        private void FixedUpdate()
        {
            //Destroy the object when at the pool height
            if (!m_splashed && transform.position.y < GameSettings.POOL_HEIGHT)
            {
                // create particle effect
                if (m_particles.Length > 0)
                    Instantiate(m_particles[Random.Range(0, m_particles.Length)], transform.position, Quaternion.identity);

                // play sound effect
                GetComponent<SFXController>().PlayRandomSoundClipFromCollectionAtPosition("Fish Water Splash", transform.position);

                // destroy the fish after a short time
                Destroy(gameObject, m_timer);

                m_splashed = true;
            }
        }
    }
}

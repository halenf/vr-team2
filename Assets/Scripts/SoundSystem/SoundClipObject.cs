using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace Sound
    {
        public class SoundClipObject : MonoBehaviour
        {
            private AudioSource m_source;
            private bool m_isPlaying = false;

            public void Init(SFXController.SoundClip soundClip)
            {
                // get source component
                m_source = GetComponent<AudioSource>();
                m_source.clip = soundClip.audioClip;
                m_source.loop = false;

                // set variables
                m_source.volume = soundClip.volume;
                m_source.pitch = soundClip.pitch;
                m_source.maxDistance = soundClip.range;

                // play clip
                m_source.Play();
                m_isPlaying = true;
            }

            public void Update()
            {
                // destroy SoundClipObject when clip finishes playing
                if (m_isPlaying && !m_source.isPlaying)
                    Destroy(gameObject);
            }
        }
    }
}

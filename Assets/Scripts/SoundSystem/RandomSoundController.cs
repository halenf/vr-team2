using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace Sound
    {
        public class RandomSoundController : MonoBehaviour
        {
            [SerializeField] private bool m_enabled = true;
            [SerializeField] private string m_soundCollectionName;
            [Tooltip("The min time between when a clip should play randomly from the above Clip Collection.")]
            [SerializeField] private float m_clipFrequencyMin;
            [Tooltip("The max time between when a clip should play randomly from the above Clip Collection.")]
            [SerializeField] private float m_clipFrequencyMax;

            private SFXController m_sfxController;
            private float m_timer = 0;

            // Start is called before the first frame update
            void Start()
            {
                m_sfxController = GetComponentInChildren<SFXController>();
                SetRandomTimer();
            }

            // Update is called once per frame
            void Update()
            {
                if (m_enabled)
                {
                    if (m_timer <= 0)
                    {
                        m_sfxController.PlayRandomSoundClipFromCollection(m_soundCollectionName);
                        SetRandomTimer();
                    }
                    else
                        m_timer -= Time.deltaTime;
                }
            }

            public void ToggleRandomSoundEffects(bool value = false)
            {
                // if changing from false to true, start the timer;
                if (!m_enabled && value)
                    SetRandomTimer();

                m_enabled = value;
            }

            private void SetRandomTimer()
            {
                m_timer = Random.Range(m_clipFrequencyMin, m_clipFrequencyMax);
            }
        }
    }
}

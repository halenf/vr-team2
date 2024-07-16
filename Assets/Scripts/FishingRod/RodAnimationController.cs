using FishingGame.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RodState = FishingGame.FishingRod.RodController.RodState;

namespace FishingGame
{
    namespace FishingRod
    {
        public class RodAnimationController : MonoBehaviour
        {
            private SFXController m_sfxController;
            private RodController m_rod;

            [Header("Reeling Sound Timer")]
            [SerializeField] private float m_startReelBufferTime;
            [SerializeField] private float m_minReelSoundTime;
            [SerializeField] private float m_maxReelSoundTime;
            private float m_reelSoundTimer;

            private bool m_lineIsBeingMoved = false;

            // Start is called before the first frame update
            void Start()
            {
                m_sfxController = GetComponent<SFXController>();
                m_rod = GetComponent<RodController>();
            }

            // Update is called once per frame
            void Update()
            {
                // the frame the rod is cast play sound effects
                if (m_rod.rodState == RodState.Casting)
                {
                    m_sfxController.PlayRandomSoundClipFromCollection("Line Casting Sounds");
                    m_sfxController.PlaySoundClip("Misc Line Sounds", "Line Thrown");
                    m_sfxController.PlaySoundClip("Misc Line Sounds", "Lock Release Line");
                }

                // check the frame when the line starts moving and play a sound
                if (!m_lineIsBeingMoved && m_rod.lineIsBeingMoved)
                {
                    m_sfxController.PlayRandomSoundClipFromCollection("Reeling Sounds");
                    SetTimerValue();
                    m_reelSoundTimer += m_startReelBufferTime;
                }

                m_lineIsBeingMoved = m_rod.lineIsBeingMoved;

                // while the rod line is being affected in some way
                if (m_lineIsBeingMoved)
                {
                    if (m_reelSoundTimer <= 0)
                    {
                        m_sfxController.PlayRandomSoundClipFromCollection("Reeling Sounds");
                        SetTimerValue();
                    }
                    else
                        m_reelSoundTimer -= Time.deltaTime;
                }
            }

            private void SetTimerValue()
            {
                m_reelSoundTimer = Random.Range(m_minReelSoundTime, m_maxReelSoundTime);
            }
        }
    }
}

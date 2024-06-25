//using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FishingGame
{
    namespace Sound
    {
        public class SFXController : MonoBehaviour
        {
            [System.Serializable]
            public class SoundClipCollection
            {
                [SerializeField] private string m_collectionName;
                [SerializeField] private List<SoundClip> m_soundClips;
                public string collectionName { get { return m_collectionName; } }
                public List<SoundClip> soundClips { get { return m_soundClips; } }
            }

            [System.Serializable]
            public class SoundClip
            {
                [SerializeField] private string m_clipName;
                [SerializeField] private AudioClip m_audioClip;
                [SerializeField, Range(0f, 1f)] private float m_volume;
                [SerializeField, Range(-3f, 3f)] private float m_pitch;

                public string clipName { get { return m_clipName; } }
                public AudioClip audioClip { get { return m_audioClip; } }
                public float volume { get { return m_volume; } }
                public float pitch { get { return m_pitch; } }
            }

            [SerializeField] private List<SoundClipCollection> m_clipCollections;

            private void PlayAudioClip(SoundClip soundClip)
            {
                SoundClipObject newSoundClip = Instantiate(new SoundClipObject(), transform);
                newSoundClip.Init(soundClip);
            }

            /// <summary>
            /// Play a specified sound clip from a specified collection.
            /// </summary>
            /// <param name="collectionName"></param>
            /// <param name="clipName"></param>
            public void PlaySoundClip(string collectionName, string clipName)
            {
                PlayAudioClip(m_clipCollections.Find(collection => collection.collectionName == collectionName).soundClips.Find(clip => clip.clipName == clipName));
            }

            /// <summary>
            /// Play a random sound clip from a specified collection.
            /// </summary>
            /// <param name="collectionName"></param>
            public void PlayRandomSoundClipFromCollection(string collectionName)
            {
                List<SoundClip> soundClips = m_clipCollections.Find(collection => collection.collectionName == collectionName).soundClips;
                PlayAudioClip(soundClips[Random.Range(0, soundClips.Count)]);
            }
        }
    }
}
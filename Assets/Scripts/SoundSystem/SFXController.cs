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
                [SerializeField, Range(0f, 1f)] private float m_volume = 1;
                [SerializeField, Range(-3f, 3f)] private float m_pitch = 1;
                [SerializeField] private float m_range = 500f;

                public string clipName { get { return m_clipName; } }
                public AudioClip audioClip { get { return m_audioClip; } }
                public float volume { get { return m_volume; } }
                public float pitch { get { return m_pitch; } }
                public float range { get { return m_range; } }
            }

            [SerializeField] private SoundClipObject m_soundClipObjectPrefab;
            [SerializeField] private List<SoundClipCollection> m_clipCollections;

            private void PlayAudioClip(SoundClip soundClip)
            {
                SoundClipObject newSoundClipObject = Instantiate(m_soundClipObjectPrefab, transform);
                newSoundClipObject.name = soundClip.clipName;
                newSoundClipObject.Init(soundClip);
            }

            private void PlayAudioClipAtPosition(SoundClip soundClip, Vector3 position)
            {
                SoundClipObject newSoundClipObject = Instantiate(m_soundClipObjectPrefab, position, Quaternion.identity);
                newSoundClipObject.name = soundClip.clipName;
                newSoundClipObject.Init(soundClip);
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
            
            public void PlaySoundClipAtPosition(string collectionName, string clipName, Vector3 position)
            {
                PlayAudioClipAtPosition(m_clipCollections.Find(collection => collection.collectionName == collectionName).soundClips.Find(clip => clip.clipName == clipName), position);
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

            public void PlayRandomSoundClipFromCollectionAtPosition(string collectionName, Vector3 position)
            {
                List<SoundClip> soundClips = m_clipCollections.Find(collection => collection.collectionName == collectionName).soundClips;
                PlayAudioClipAtPosition(soundClips[Random.Range(0, soundClips.Count)], position);
            }
        }
    }
}
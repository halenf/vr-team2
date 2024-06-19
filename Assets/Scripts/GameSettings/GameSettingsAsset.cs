using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    [CreateAssetMenu]
    public class GameSettingsAsset : ScriptableObject
    {       
        [SerializeField] private Vector3 m_poolOrigin;
        [SerializeField] private float m_poolRadius;
        [SerializeField] private int m_maxFishCount;

        public Vector3 poolOrigin { get { return m_poolOrigin; } }
        public float poolRadius { get { return m_poolRadius; } }
        public int maxFishCount { get { return m_maxFishCount; } }
    }
}

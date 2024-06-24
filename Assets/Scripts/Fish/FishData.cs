using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    [CreateAssetMenu(fileName = "FishData", menuName = "FishingGame/FishData", order = 0)]
    public class FishData : ScriptableObject
    {
        [System.Serializable]
        public class Constraint
        {
            [SerializeField] private float m_min;
            [SerializeField] private float m_max;
            public float min { get { return m_min; } }
            public float max { get { return m_max; } }
        }

        [System.Serializable]
        public enum FishSilhouetteSize
        {
            Small,
            Medium,
            Large,
            Huge
        }

        [Header("Fish Object")]
        [SerializeField] private GameObject m_model;
        [SerializeField] private FishSilhouetteSize m_silhouetteSize;

        [Header("Core Data")]
        [SerializeField] private string m_speciesName; // key
        [Space()]
        [SerializeField] private Constraint m_weight;
        [SerializeField] private Constraint m_length;

        [Header("AI Data")]
        [SerializeField] private Constraint m_lifeTime;
        [SerializeField] private Constraint m_swimSpeed;
        [SerializeField] private Constraint m_chaseSpeed;
        [SerializeField] private Constraint m_spookSpeed;
        [SerializeField] private Constraint m_swimRange;
        [SerializeField] private Constraint m_swimDetectionRange;
        [SerializeField] private Constraint m_bobberDetectionRange;
        [SerializeField] private Constraint m_playerDetectionRange;
        [SerializeField] private Constraint m_swimWaitTime;
        [SerializeField] private Constraint m_bobberWaitTime;
        [SerializeField] private Constraint m_pullStrength;

        // object
        public GameObject model { get { return m_model; } }
        public FishSilhouetteSize silhouetteSize { get { return m_silhouetteSize; } }

        // core
        public string speciesName { get { return m_speciesName; } }
        public Constraint weight { get { return m_weight; } }
        public Constraint length { get { return m_length; } }

        // ai
        public Constraint lifeTime { get { return m_lifeTime; } }
        public Constraint swimSpeed { get { return m_swimSpeed; } }
        public Constraint chaseSpeed { get { return m_chaseSpeed; } }
        public Constraint spookSpeed { get { return m_spookSpeed; } }
        public Constraint swimRange { get { return m_swimRange; } }
        public Constraint swimDetectionRange { get { return m_swimDetectionRange; } }
        public Constraint bobberDetectionRange { get { return m_bobberDetectionRange; } }
        public Constraint playerDetectionRange { get { return m_playerDetectionRange; } }
        public Constraint swimWaitTime { get { return m_swimWaitTime; } }
        public Constraint bobberWaitTime { get { return m_bobberWaitTime; } }
        public Constraint pullStrength { get { return m_pullStrength; } }
    }
}

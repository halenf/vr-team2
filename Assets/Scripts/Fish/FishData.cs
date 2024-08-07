using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    [CreateAssetMenu(fileName = "FishData", menuName = "FishData", order = 0)]
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
        [SerializeField] private Sprite m_sprite;
        [SerializeField] private FishSilhouetteSize m_silhouetteSize;

        [Header("Core Data")]
        [SerializeField] private string m_speciesName; // key
        [SerializeField] private string m_scientificName;
        [SerializeField, TextArea] private string m_aboutDetails;
        [Space()]
        [SerializeField, Tooltip("Kilograms")] private Constraint m_weight;
        [SerializeField, Tooltip("Metres")] private Constraint m_length;

        [Header("AI Data")]
        [SerializeField, Tooltip("Seconds")] private Constraint m_lifeTime;
        [SerializeField, Tooltip("Units per second")] private Constraint m_swimSpeed;
        [SerializeField, Tooltip("Units per second")] private Constraint m_chaseSpeed;
        [SerializeField, Tooltip("Units per second")] private Constraint m_spookSpeed;
        [SerializeField, Tooltip("Units")] private Constraint m_swimRange;
        [SerializeField, Tooltip("Units")] private Constraint m_spookRange;
        [SerializeField, Tooltip("Units")] private Constraint m_swimDetectionRange;
        [SerializeField, Tooltip("Units")] private Constraint m_bobberDetectionRange;
        [SerializeField, Tooltip("Seconds")] private Constraint m_swimWaitTime;
        [SerializeField, Tooltip("Seconds")] private Constraint m_bobberWaitTime;
        [SerializeField, Tooltip("Multiplier")] private Constraint m_pullStrength;

        // object
        public GameObject model { get { return m_model; } }
        public Sprite sprite { get { return m_sprite; } }
        public FishSilhouetteSize silhouetteSize { get { return m_silhouetteSize; } }

        // core
        public string speciesName { get { return m_speciesName; } }
        public string scientificName { get { return m_scientificName; } }
        public string aboutDetails { get { return m_aboutDetails; } }
        public Constraint weight { get { return m_weight; } }
        public Constraint length { get { return m_length; } }

        // ai
        public Constraint lifeTime { get { return m_lifeTime; } }
        public Constraint swimSpeed { get { return m_swimSpeed; } }
        public Constraint chaseSpeed { get { return m_chaseSpeed; } }
        public Constraint spookSpeed { get { return m_spookSpeed; } }
        public Constraint swimRange { get { return m_swimRange; } }
        public Constraint spookRange { get { return m_spookRange; } }
        public Constraint swimDetectionRange { get { return m_swimDetectionRange; } }
        public Constraint bobberDetectionRange { get { return m_bobberDetectionRange; } }
        public Constraint swimWaitTime { get { return m_swimWaitTime; } }
        public Constraint bobberWaitTime { get { return m_bobberWaitTime; } }
        public Constraint pullStrength { get { return m_pullStrength; } }
    }
}

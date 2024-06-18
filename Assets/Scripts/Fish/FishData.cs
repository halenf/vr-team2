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
        
        [SerializeField] private GameObject m_fishModel;

        [Header("Core Data")]
        [SerializeField] private string m_speciesName; // key
        [Space()]
        [SerializeField] private Constraint m_weight;
        [SerializeField] private Constraint m_length;

        [Header("AI Data")]
        [SerializeField] private Constraint m_lifeTime;
        [SerializeField] private Constraint m_swimSpeed;
        [SerializeField] private Constraint m_spookSpeed;
        [SerializeField] private Constraint m_swimRange;
        [SerializeField] private Constraint m_swimWaitTime;
        [SerializeField] private Constraint m_rodDetectionRange;
        [SerializeField] private Constraint m_rodWaitTime;

        [Header("Reeling Data")]
        [SerializeField] private Constraint m_pullStrength;

        public GameObject fishModel { get { return m_fishModel; } }

        // core
        public string speciesName { get { return m_speciesName; } }
        public Constraint weight { get { return m_weight; } }
        public Constraint length { get { return m_length; } }

        // ai
        public Constraint lifeTime { get { return m_lifeTime; } }
        public Constraint swimSpeed { get { return m_swimSpeed; } }
        public Constraint spookSpeed { get { return m_spookSpeed; } }
        public Constraint swimRange { get { return m_swimRange; } }
        public Constraint swimWaitTime { get { return m_swimWaitTime; } }
        public Constraint rodDetectionRange { get { return m_rodDetectionRange; } }
        public Constraint rodWaitTime { get { return m_rodWaitTime; } }

        // reeling
        public Constraint pullStrength { get { return m_pullStrength; } }
    }
}

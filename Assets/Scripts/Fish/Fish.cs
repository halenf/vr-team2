using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class Fish : MonoBehaviour
    {              
        // data
        [SerializeField] private FishData m_data;
        public FishData data { get { return m_data; } }

        // properties
        private float m_weight, m_length;

        // property accessors
        public string speciesName { get { return m_data.speciesName; } }
        public float weight { get { return m_weight; } }
        public float length { get { return m_length; } }

        public void Init(FishData data)
        {
            m_data = data;
        }

        // Start is called before the first frame update
        void Start()
        {
            m_weight = GetConstraint(m_data.weight);
            m_length = GetConstraint(m_data.length);
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Returns a random float within the range of the Constraint's min and max values.
        /// Uses UnityEngine's Random class.
        /// </summary>
        /// <param name="variable">The Constraint to take the min and max values from.</param>
        public float GetConstraint(FishData.Constraint variable)
        {
            return Random.Range(variable.min, variable.max);
        }
    }
}

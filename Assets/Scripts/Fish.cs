using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class Fish : MonoBehaviour
    {
        public Fish(FishData data)
        {
            m_data = data;
        }
        
        // data
        [SerializeField] private FishData m_data;
        public FishData data { get { return m_data; } }

        // state machine
        private FishStateMachine m_stateMachine;

        // properties
        private float m_weight, m_length, m_swimSpeed, m_spookSpeed, m_swimRange, m_waitTime;

        // property accessors
        public string speciesName { get { return m_data.speciesName; } }
        public float weight { get { return m_weight; } }
        public float length { get { return m_length; } }
        public float swimSpeed { get { return m_swimSpeed; } }
        public float spookSpeed { get { return m_spookSpeed; } }
        public float swimRange { get { return m_swimRange; } }
        public float waitTime { get { return m_waitTime; } }


        // Start is called before the first frame update
        void Start()
        {
            m_stateMachine = new FishStateMachine();
            m_stateMachine.Start();
        }

        // Update is called once per frame
        void Update()
        {
            m_stateMachine.Update(this);
        }
    }
}

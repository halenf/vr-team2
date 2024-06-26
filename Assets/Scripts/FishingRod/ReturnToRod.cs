using FishingGame.FishingRod;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace Objects
    {
        public class ReturnToRod : MonoBehaviour
        {
            private float m_timer;
            private float m_minimumVelocity;
            private Rigidbody m_rb;
            private RodController m_rod;
            private float m_counter;

            public void Init(float timer, float minVelocity)
            {
                m_timer = timer;
                m_minimumVelocity = minVelocity;
            }

            private void Start()
            {
                m_rb = GetComponent<Rigidbody>();
                m_rod = FindObjectOfType<RodController>();
            }

            private void Update()
            {
                if (m_rb.velocity.magnitude < m_minimumVelocity)
                {
                    m_counter += Time.deltaTime;
                }

                if (m_counter > m_timer)
                {
                    ReturnThisToRod();
                }
            }

            public void ReturnThisToRod()
            {
                //m_rod.AttachObjectToDisplayPoint(transform);
                Destroy(this);
            }
        }
    }
}

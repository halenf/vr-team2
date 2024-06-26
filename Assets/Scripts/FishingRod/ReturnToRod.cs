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
            [SerializeField] private float m_timer;
            [SerializeField] private float m_minimumVelocity;

            private Rigidbody m_rb;
            private RodController m_rod;
            private float m_counter;
            private bool m_attachedToObject;

            private void Awake()
            {
                m_rb = GetComponent<Rigidbody>();
                m_rod = FindObjectOfType<RodController>();
                m_counter = 0;
                m_attachedToObject = true;
            }

            private void Update()
            {
                if (!m_attachedToObject)
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
            }

            public void Detach()
            {
                m_attachedToObject = false;
            }

            public void ReturnThisToRod()
            {
                m_rod.AttachObjectToDisplayPoint(transform);
                m_counter = 0;
                m_attachedToObject = true;
            }
        }
    }
}

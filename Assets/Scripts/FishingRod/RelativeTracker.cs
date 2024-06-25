using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace FishingRod
    {
        public class RelativeTracker : MonoBehaviour
        {
            [SerializeField] private Transform m_target;
            [SerializeField] private bool m_useX;
            private Vector2 m_direction;
            private float m_prevRotation = 0.0f;
            private float m_rotVelo = 0.0f;
            public float getRotationalVelocity { get { return m_rotVelo; } }
            // Start is called before the first frame update
            void Start()
            {

            }

            // Update is called once per frame
            void Update()
            {
                if (m_useX)
                {
                    m_direction = (new Vector2(m_target.position.z, m_target.position.y) - new Vector2(transform.position.z, transform.position.y)).normalized;
                }
                else
                {
                    m_direction = (new Vector2(m_target.position.x, m_target.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized;
                }
                float currentRotation = Mathf.Atan(m_direction.y / m_direction.x);

                if (m_direction.x < 0)
                    currentRotation += Mathf.PI;
                else if (m_direction.y < 0)
                    currentRotation += Mathf.PI * 2;

                m_rotVelo = m_prevRotation - currentRotation;
                m_prevRotation = currentRotation;
            }

            private void OnDrawGizmos()
            {
                Vector3 pos = transform.position;
                if (m_useX)
                    pos += new Vector3(0, m_direction.y * 0.05f, m_direction.x * 0.05f);
                else
                    pos += new Vector3(m_direction.x * 0.05f, m_direction.y * 0.05f, 0);
                Gizmos.DrawWireSphere(pos, 0.05f);
            }
        }
    }
}
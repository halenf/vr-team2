using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FishingGame
{
    namespace Objects
    {
        [RequireComponent(typeof(Rigidbody))]
        public class BuoyantObject : MonoBehaviour
        {
            [SerializeField] private float m_underwaterDrag;
            [SerializeField] private float m_underwaterAngularDrag;
            [SerializeField] private float m_airDrag;
            [SerializeField] private float m_airAngularDrag;
            private Rigidbody m_rb;
            private bool m_isUnderwater;

            public bool IsUnderwater
            {
                get
                {
                    return transform.position.y < (GameSettings.POOL_HEIGHT + 0.2f);
                }
            }

            [SerializeField] private float m_floatingPower;
            // Start is called before the first frame update
            void Start()
            {
                m_rb = GetComponent<Rigidbody>();
            }

            // Update is called once per frame
            void Update()
            {
                float diff = transform.position.y - GameSettings.POOL_HEIGHT;

                if (diff < 0)
                {
                    m_rb.AddForceAtPosition(Vector3.up * m_floatingPower * Mathf.Abs(diff), transform.position, ForceMode.Force);
                    if (!m_isUnderwater)
                    {
                        m_isUnderwater = true;
                        SwitchState();
                    }
                }
                else if (m_isUnderwater)
                {
                    m_isUnderwater = false;
                    SwitchState();
                }
            }
            private void SwitchState()
            {
                if (m_isUnderwater)
                {
                    m_rb.drag = m_underwaterDrag;
                    m_rb.angularDrag = m_underwaterAngularDrag;
                }
                else
                {
                    m_rb.drag = m_airDrag;
                    m_rb.angularDrag = m_underwaterAngularDrag;
                }
            }
        }
    }
}
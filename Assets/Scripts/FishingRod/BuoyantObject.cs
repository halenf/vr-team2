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
            [SerializeField, Min(0.01f)] private float m_forceStrength;
            [SerializeField, Min(0)] private float m_minimumForce;
#if UNITY_EDITOR
            [SerializeField] private bool m_isUnderwater;
#endif

            private Rigidbody m_rb;

            void Start()
            {
                m_rb = GetComponent<Rigidbody>();
            }

            // Update is called once per frame
            void Update()
            {
                float delta = transform.position.y - GameSettings.POOL_HEIGHT;
                bool isUnderwater = delta < 0;
#if UNITY_EDITOR
                m_isUnderwater = isUnderwater;
#endif

                if (isUnderwater)
                    ApplyFloatationForce(-delta);
            }

            private void ApplyFloatationForce(float value)
            {
                m_rb.AddForce(new Vector3(0, (value * m_forceStrength + m_minimumForce) * Time.deltaTime), ForceMode.VelocityChange);
            }
        }
    }
}
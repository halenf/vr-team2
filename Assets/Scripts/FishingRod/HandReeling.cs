using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FishingGame
{
    namespace FishingRod
    {
        public class HandReeling : MonoBehaviour
        {
            [SerializeField] private Transform m_refFrame;
            [SerializeField] private Transform m_target;
            private float lastAngle;
            public float rotationSpeed;
            private RodController m_rodControl;

            private bool m_enabled;
            void Start()
            {
                m_rodControl = FindObjectOfType<RodController>();
                if (!m_rodControl)
                {
                    Debug.Log("Reel couldn't find the rod! Please add the Rod Controller to the scene.");
                    Destroy(this);
                    return;
                }
                if (!m_refFrame)
                    m_refFrame = transform;
            }

            void Update()
            {
                if (m_enabled)
                {
                    Vector3 trans = (m_refFrame.worldToLocalMatrix * m_target.localToWorldMatrix).GetPosition();
                    Vector2 targetDir = new Vector2(trans.z, trans.y).normalized;
                    float dirAsAngle = Mathf.Atan(targetDir.y / targetDir.x);
                    if (targetDir.x < 0) dirAsAngle += Mathf.PI;
                    else if (targetDir.y < 0) dirAsAngle += Mathf.PI * 2;

                    rotationSpeed = Mathf.Clamp(lastAngle - dirAsAngle, -Mathf.PI / 2, Mathf.PI / 2);
                    lastAngle = dirAsAngle;

                    m_rodControl.setReelVelo = rotationSpeed;
                }
            }
            public void ReelGrabbed(InputAction.CallbackContext action)
            {
                switch(action.phase)
                {
                    case InputActionPhase.Started:
                        m_enabled = true;
                        break;
                    case InputActionPhase.Canceled:
                        m_rodControl.setReelVelo = 0;
                        m_enabled = false;
                        break;
                }
            }
        }
    }
}

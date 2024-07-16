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
            [Header("Bools")]
            private bool m_cranking;
            private bool m_active;

            [Header("Transforms")]
            [SerializeField] private Transform m_refFrame;
            [SerializeField] private Transform m_target;

            [Header("Angles")]
            private float lastAngle;
            public float rotationSpeed;

            private RodController m_rodControl;

            void Start()
            {
                //Get everything required for this script
                m_rodControl = FindObjectOfType<RodController>();
                if (!m_rodControl)
                {
                    Debug.Log("Reel couldn't find the rod! Please add the Rod Controller to the scene.");
                    Destroy(this);
                    return;
                }
                //if no reference frame is given, use this as the reference frame
                if (!m_refFrame)
                    m_refFrame = transform;
            }

            void Update()
            {
                //while we are getting player input
                if (m_cranking && m_active)
                {
                    //Get the target's position relative to the reference frame's position and rotation
                    Vector3 trans = (m_refFrame.worldToLocalMatrix * m_target.localToWorldMatrix).GetPosition();
                    //Get the relative direction in the z and y axi of the target
                    Vector2 targetDir = new Vector2(trans.z, trans.y).normalized;
                    //convert that direction to an angle
                    float dirAsAngle = Mathf.Atan(targetDir.y / targetDir.x);
                    //Add values to fix the angle going in reverse
                    if (targetDir.x < 0) dirAsAngle += Mathf.PI;
                    else if (targetDir.y < 0) dirAsAngle += Mathf.PI * 2;

                    //get difference in the angle from the last frame
                    rotationSpeed = Mathf.Clamp(lastAngle - dirAsAngle, -Mathf.PI / 2, Mathf.PI / 2);
                    lastAngle = dirAsAngle;

                    //pass that difference as the reel speed
                    m_rodControl.setReelVelo = rotationSpeed;
                }
            }
            public void ReelGrabbed(InputAction.CallbackContext action)
            {
                if (!m_active)
                    return;
                switch(action.phase)
                {
                    case InputActionPhase.Started:
                        m_cranking = true;
                        break;
                    case InputActionPhase.Canceled:
                        m_rodControl.setReelVelo = 0;
                        m_cranking = false;
                        break;
                }
            }
            public void SetActive(bool value)
            {
                m_active = value;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FishingGame.Player;

namespace FishingGame
{
    namespace ControlHook
    {
        public class RodControllerInputHook : MonoBehaviour
        {
            private RodController m_base;
            private void Start()
            {
                m_base = FindObjectOfType<RodController>();
            }
            public void RodLock(InputAction.CallbackContext action)
            {
                switch (action.phase)
                {
                    case InputActionPhase.Performed:
                        Debug.Log("Locking line.");
                        m_base.isLineLocked = 1;
                        break;
                    case InputActionPhase.Canceled:
                        Debug.Log("Unlocking line.");
                        m_base.isLineLocked = 2;
                        break;

                }
                /*        if (action.started)
                        {
                            //Debug.Log("Lock state: " + m_base.isLineLocked);
                        }
                        if(action.performed)
                        {
                            Debug.Log("middle of action");
                        }
                        if (action.canceled)
                        {
                            m_base.isLineLocked = false;
                            //Debug.Log("Lock state: " + m_base.isLineLocked);
                        }*/
            }
            public void PassControllerVelo(InputAction.CallbackContext action)
            {
                //LogAction(action);
                m_base.setHandVelocity = action.ReadValue<Vector3>();
            }
            private void LogAction(InputAction.CallbackContext action)
            {
                Debug.Log("Input logged: " + action.control);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FishingGame
{
    namespace FishingRod
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
                    case InputActionPhase.Canceled:
                        Debug.Log("Unlocking line.");
                        if(m_base.rodState == RodController.RodState.Mounted)
                            m_base.rodState = RodController.RodState.Casting;
                        break;

                }
            }
            public void PassControllerVelo(InputAction.CallbackContext action)
            {
                //LogAction(action);
                m_base.setHandVelocity = action.ReadValue<Vector3>();
            }
            public void PassStickY(InputAction.CallbackContext action)
            {
                m_base.setReelVelo = action.ReadValue<Vector2>().y;
            }
            public void GripButton(InputAction.CallbackContext action)
            {
                switch (action.phase)
                {
                    case InputActionPhase.Started:
                        m_base.rodState = RodController.RodState.Reeling;
                        break;
                    case InputActionPhase.Canceled:
                        m_base.rodState = RodController.RodState.Cast;
                        break;
                }
            }
            private void LogAction(InputAction.CallbackContext action)
            {
                Debug.Log("Input logged: " + action.control);
            }
        }
    }
}
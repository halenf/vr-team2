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
                    case InputActionPhase.Canceled:
                        Debug.Log("Unlocking line.");
                        if(m_base.getRodState == RodController.state.Mounted)
                            m_base.getRodState = RodController.state.CastingOut;
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
                        m_base.getRodState = RodController.state.Reeling;
                        break;
                    case InputActionPhase.Canceled:
                        m_base.getRodState = RodController.state.Cast;
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
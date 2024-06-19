using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FishingGame
{
    namespace Testing
    {
        public class BobberMover : MonoBehaviour
        {
            public Transform player;

            private bool m_moving;

            private void Update()
            {
                if (m_moving)
                    transform.position += (player.transform.position - transform.position).normalized * 2.0f * Time.deltaTime;
            }

            public void MoveTowards(InputAction.CallbackContext action)
            {
                switch (action.phase)
                {
                    case InputActionPhase.Started:
                        m_moving = true;
                        break;
                    case InputActionPhase.Canceled:
                        m_moving = false;
                        break;
                }
            }
        }
    }
}
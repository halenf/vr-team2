using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FishingGame
{
    namespace FishingRod
    {

        public class FlyFishing : MonoBehaviour
        {
            private bool m_lineHeld;
            [SerializeField] private Transform m_target;
            private float m_lastDistance;
            [SerializeField, Min(0.001f)] private float m_scale;
            private RodController m_rodControl;
            [SerializeField] private bool m_allowPushback;
            private void Start()
            {
                //Find the rod before trying anything
                m_rodControl = FindObjectOfType<RodController>();
                if (!m_rodControl)
                {
                    Debug.Log("Reel couldn't find the rod! Please add the Rod Controller to the scene.");
                    Destroy(this);
                    return;
                }
            }
            public void HoldLine(InputAction.CallbackContext action)
            {
                //Enable or disable this input as long as a button is held
                switch (action.phase)
                {
                    case InputActionPhase.Performed:
                        m_lineHeld = true;
                        break;
                    case InputActionPhase.Canceled:
                        m_lineHeld = false;
                        break;
                }
            }
            void Update()
            {
                //Get the distance between this and the target
                float dis = (m_target.position - transform.position).magnitude;
                //if the new distance is greater.
                if(dis > m_lastDistance)
                    //reel the rod in
                    m_rodControl.setReelVelo = -dis * m_scale;
                //else if we are allowing pushback
                else if (dis < m_lastDistance && m_allowPushback)
                    //push it back out at a reduced speed
                    m_rodControl.setReelVelo = -dis * m_scale / 2;
                //updated the last distance
                m_lastDistance = dis;
            }
        }
    }
}
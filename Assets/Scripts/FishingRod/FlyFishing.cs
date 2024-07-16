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
            private bool m_active;
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
                if (!m_active)
                    return;
                //Enable or disable this input as long as a button is held
                switch (action.phase)
                {
                    case InputActionPhase.Performed:
                        m_lineHeld = true;
                        m_lastDistance = (m_target.position - transform.position).magnitude;
                        break;
                    case InputActionPhase.Canceled:
                        m_rodControl.setReelVelo = 0;
                        m_lineHeld = false;
                        break;
                }
            }
            void Update()
            {
                if (m_lineHeld && m_active)
                {
                    //Get the distance between this and the target
                    float dis = (m_target.position - transform.position).magnitude;
                    //then the difference in distance from the last frame
                    float dif = dis - m_lastDistance;
                    Debug.Log("Dif" + dif);
                    //if the difference has grown.
                    if (dif > 0)
                        //reel the rod in
                        m_rodControl.setReelVelo = -dif * m_scale;
                    //else if we are allowing pushback
                    else if (m_allowPushback)
                        //push it back out at a reduced speed
                        m_rodControl.setReelVelo = -dif * m_scale / 2.0f;

                    //updated the last distance
                    m_lastDistance = dis;
                }
            }
            public void SetActive(bool value)
            {
                m_active = value;
            }
        }
    }
}
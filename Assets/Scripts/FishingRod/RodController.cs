using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace FishingGame
{
    namespace Player
    {
        [RequireComponent(typeof(LineRenderer))]
        [RequireComponent(typeof(ReelFish))]
        public class RodController : MonoBehaviour
        {
            [SerializeField] private int m_rodState = 0;
            public int getRodState
            {
                get { return m_rodState; }
                set { m_rodState = value; }
            }
            private Vector3 m_handVelocity;
            [SerializeField] private float m_forceScale;
            public Vector3 setHandVelocity { set { m_handVelocity = value; } }
            [SerializeField] private GameObject m_hook;
            [SerializeField] private Transform m_hookHold;
            private float m_reelVelocity;
            public float setReelVelo { set { m_reelVelocity = value; } }
            private ReelFish m_reelFish;
            [Header("Fishing Line")]
            private LineRenderer m_line;
            [SerializeField, Range(0.5f,1f)] private float m_lineTension = 0.75f;

            private void Start()
            {
                m_line = GetComponent<LineRenderer>();
                m_reelFish = GetComponent<ReelFish>();
            }
            // Update is called once per frame
            void Update()
            {
                Debug.DrawRay(transform.position, new Vector3(m_handVelocity.normalized.x,0,0) * 10, Color.red);
                Debug.DrawRay(transform.position, new Vector3(0,m_handVelocity.normalized.y,0) * 10, Color.green);
                Debug.DrawRay(transform.position, new Vector3(0,0,m_handVelocity.normalized.z) * 10, Color.blue);
                Debug.DrawRay(transform.position, m_handVelocity.normalized * 10, Color.yellow);

                List<Vector3> points = new List<Vector3>();
                m_line.SetPositions(new Vector3[6]);
                points.Add(transform.position + transform.forward * -0.05f);
                points.Add(m_hookHold.position + m_hookHold.forward * -0.05f + m_hookHold.up * 0.1f);
                FindLinePoints(points, 2, m_hookHold.position, m_hook.transform.position);
                FindLinePoints(points, 3, points[2], m_hook.transform.position);
                FindLinePoints(points, 2, m_hookHold.position, points[2]);
                points.Add(m_hook.transform.position);
                m_line.SetPositions(points.ToArray());

                switch (m_rodState)
                {
                    case 0:
                        //idle
                            m_lineTension = 0.75f;
                        if (m_reelVelocity < 0)
                        {
                            //a fish is hooked, start battle
                            m_rodState = 3;
                        }
                        break;
                    case 1:
                        //Reeling in
                        m_hook.transform.position = Vector3.Slerp(m_hook.transform.position, m_hookHold.position, 0.5f);
                        m_hook.GetComponent<Rigidbody>().isKinematic = true;
                        break;
                    case 2:
                        //cast rod out
                        m_hook.GetComponent<Rigidbody>().isKinematic = false;
                        m_hook.GetComponent<Rigidbody>().AddForce(m_handVelocity.normalized * m_forceScale);
                        m_rodState = 0;
                        break;
                    case 3:
                        //reeling in fish
                        m_lineTension = 0.5f;
                        m_hook.transform.position = Vector3.Lerp(m_hook.transform.position, new Vector3(m_hookHold.position.x, m_hook.transform.position.y, m_hookHold.position.z), Time.deltaTime);
                        break;
                        
                }
            }
            private void FindLinePoints(List<Vector3> points, int index, Vector3 initPos, Vector3 targPos)
            {
                //find the mid point stuff;
                float yDiff = initPos.y - targPos.y;
                Vector3 pos = (initPos + targPos) / 2.0f;
                pos.y = initPos.y - yDiff * m_lineTension;

                points.Insert(index, pos);

            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace Player
    {
        [RequireComponent(typeof(LineRenderer))]
        public class RodController : MonoBehaviour
        {
            private int m_lineLockPhase = 0;
            public int isLineLocked
            {
                get { return m_lineLockPhase; }
                set { m_lineLockPhase = value; }
            }
            private Vector3 m_handVelocity;
            [SerializeField] private float m_forceScale;
            public Vector3 setHandVelocity { set { m_handVelocity = value; } }
            [SerializeField] private GameObject m_hook;
            [SerializeField] private Transform m_hookHold;
            [Header("Fishing Line")]
            private LineRenderer m_line;
            private int m_iterations = 1;

            private void Start()
            {
                m_line = GetComponent<LineRenderer>();
            }
            // Update is called once per frame
            void Update()
            {
                Debug.DrawRay(transform.position, new Vector3(m_handVelocity.normalized.x,0,0) * 10, Color.red);
                Debug.DrawRay(transform.position, new Vector3(0,m_handVelocity.normalized.y,0) * 10, Color.green);
                Debug.DrawRay(transform.position, new Vector3(0,0,m_handVelocity.normalized.z) * 10, Color.blue);
                Debug.DrawRay(transform.position, m_handVelocity.normalized * 10, Color.yellow);

                List<Vector3> points = new List<Vector3>();
                m_line.SetPositions(new Vector3[5]);
                points.Add(m_hookHold.position);
                FindLinePoints(points, 1, m_hookHold.position, m_hook.transform.position);
                FindLinePoints(points, 2, points[1], m_hook.transform.position);
                FindLinePoints(points, 1, m_hookHold.position, points[1]);
                points.Add(m_hook.transform.position);
                m_line.SetPositions(points.ToArray());

                switch (m_lineLockPhase)
                {
                    case 1:
                        m_hook.transform.position = Vector3.Slerp(m_hook.transform.position, m_hookHold.position, 0.5f);
                        m_hook.GetComponent<Rigidbody>().isKinematic = true;
                        break;
                    case 2:
                        //cast rod out
                        m_hook.GetComponent<Rigidbody>().isKinematic = false;
                        m_hook.GetComponent<Rigidbody>().AddForce(m_handVelocity.normalized * m_forceScale);
                        m_lineLockPhase = 0;
                        break;
                }
            }
            private void FindLinePoints(List<Vector3> points, int index, Vector3 initPos, Vector3 targPos)
            {
                //find the mid point stuff;
                float yDiff = initPos.y - targPos.y;
                Vector3 pos = (initPos + targPos) / 2.0f;
                pos.y = initPos.y - yDiff * 0.75f;

                points.Insert(index, pos);

            }
        }
    }
}
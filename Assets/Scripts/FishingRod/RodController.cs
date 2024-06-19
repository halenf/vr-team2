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
        //Controls player interactions with the fishing rod
        public class RodController : MonoBehaviour
        {
            //States
            public enum state
            {
                Cast,
                Mounted,
                CastingOut,
                Reeling
            }
            private state m_rodState = 0;
            public state getRodState
            {
                get { return m_rodState; }
                set { m_rodState = value; }
            }
            //Hook
            [Header("Bobber")]
            [SerializeField] private GameObject m_bobber;
            private Transform m_bobberHold;
            [SerializeField] private Transform m_rodTip;
            //Casting
            private Vector3 m_handVelocity;
            public Vector3 setHandVelocity { set { m_handVelocity = value; } }
            [Header("Casting")]
            [SerializeField] private float m_forceScale;
            //Reeling
            private float m_reelVelocity;
            public float setReelVelo { set { m_reelVelocity = value; } }
            [Header("Reeling")]
            [SerializeField] private float m_catchRadius;
            [SerializeField] private float m_pullPower;
            private ReelFish m_reelFish;
            [Header("Fishing Line Visuals")]
            private LineRenderer m_line;
            [SerializeField, Range(0.5f, 1f)] private float m_lineTension = 0.75f;

            private void Start()
            {
                //Get the line and reel fish components
                m_line = GetComponent<LineRenderer>();
                m_reelFish = GetComponent<ReelFish>();
                //Get the parent of the bobber, this is used for mounting the bobber
                m_bobberHold = m_bobber.transform.parent;
                //Match the rbs
                Rigidbody targRb = m_bobberHold.GetComponent<Rigidbody>();
                Rigidbody hRb = m_bobber.GetComponent<Rigidbody>();
                targRb.mass = hRb.mass;
                targRb.drag = hRb.drag;
                targRb.angularDrag = hRb.angularDrag;
                //mount it for safety
                MountBobber();
            }
            void Update()
            {
                switch (m_rodState)
                {
                    case state.Cast:
                        //Set the line tension to a regular amount
                        m_lineTension = 0.75f;
                        //Check for any imput on the control stick
                        if (m_reelVelocity < 0)
                        {
                            m_rodState = state.Reeling;
                        }
                        //When the player pulls the controller back, mount the bobber - change this
                        if (Vector3.Distance(m_bobber.transform.position, transform.position) < m_catchRadius && (m_handVelocity.z + m_handVelocity.y) / 2 > m_pullPower)
                        {
                            MountBobber();
                        }
                        DrawFishingLine();
                        break;
                    case state.Mounted:
                        //Just draws a line along the rod to the bobber
                        List<Vector3> points = new List<Vector3>(3);
                        m_line.positionCount = 3;
                        points.Add(transform.position + transform.forward * -0.05f);
                        points.Add(m_rodTip.position + m_rodTip.forward * -0.05f + m_rodTip.up * 0.1f);
                        points.Add(m_bobber.transform.position);
                        m_line.SetPositions(points.ToArray());
                        break;
                    case state.CastingOut:
                        //Unparent the bobber from its mount
                        m_bobber.transform.SetParent(null);
                        m_bobber.GetComponent<Rigidbody>().isKinematic = false;
                        //Apply the controller's velocity to it
                        m_bobber.GetComponent<Rigidbody>().AddForce(m_handVelocity.normalized * m_forceScale);
                        //The rod has now been cast
                        m_rodState = state.Cast;
                        break;
                    case state.Reeling:
                        //Pull the bobber towards the player
                        m_bobber.transform.position = Vector3.Lerp(m_bobber.transform.position, new Vector3(m_rodTip.position.x, m_bobber.transform.position.y, m_rodTip.position.z), Time.deltaTime);
                        //Return to the cast state when no motion is inputed
                        if (m_reelVelocity >= 0)
                        {
                            m_rodState = state.Cast;
                        }
                        //When the player pulls the controller back, mount the bobber - change this
                        if (Vector3.Distance(m_bobber.transform.position, transform.position) < m_catchRadius && (m_handVelocity.z + m_handVelocity.y) / 2 > m_pullPower)
                        {
                            MountBobber();
                        }
                        DrawFishingLine();
                        break;

                }
            }
            /// <summary>
            /// Finds the mid point between 2 points, and lowers it slightly.
            /// </summary>
            private void FindLinePoints(List<Vector3> points, int index, Vector3 initPos, Vector3 targPos)
            {
                //find the mid point stuff;
                float yDiff = initPos.y - targPos.y;
                Vector3 pos = (initPos + targPos) / 2.0f;
                pos.y = initPos.y - yDiff * m_lineTension;

                points.Insert(index, pos);
            }
            /// <summary>
            /// Draws the line when cast out
            /// </summary>
            private void DrawFishingLine()
            {
                m_lineTension = 0.75f - 0.25f * -m_reelVelocity;
                List<Vector3> points = new List<Vector3>(6);
                m_line.positionCount = 6;
                //base
                points.Add(transform.position + transform.forward * -0.05f);
                //tip
                points.Add(m_rodTip.position + m_rodTip.forward * -0.05f + m_rodTip.up * 0.1f);
                //mid
                FindLinePoints(points, 2, m_rodTip.position, m_bobber.transform.position);
                //outer
                FindLinePoints(points, 3, points[2], m_bobber.transform.position);
                //inner
                FindLinePoints(points, 2, m_rodTip.position, points[2]);
                points.Add(m_bobber.transform.position);
                m_line.SetPositions(points.ToArray());
            }
            /// <summary>
            /// Mounts the bobber to the rod
            /// </summary>
            private void MountBobber()
            {
                //pull out the hook
                m_reelFish.SurfaceFish();
                m_bobber.transform.SetParent(m_bobberHold);
                m_bobber.GetComponent<Rigidbody>().isKinematic = true;
                m_bobber.transform.position = m_bobberHold.position;
                m_rodState = state.Mounted;
            }
        }
    }
}
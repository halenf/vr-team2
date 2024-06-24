using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

namespace FishingGame
{
    namespace Player
    {
        [RequireComponent(typeof(LineRenderer))]
        //Controls player interactions with the fishing rod
        public class RodController : MonoBehaviour
        {
            //States
            public enum state
            {
                Cast,
                PreCast,
                Casting,
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
            //[SerializeField] private float m_catchRadius;
            [SerializeField] private float m_pullPower;
            [SerializeField] private Transform m_fishDisplayPoint;
            [Header("Fishing Line Visuals")]
            private LineRenderer m_line;
            [SerializeField, Range(0.5f, 1f)] private float m_lineTension = 0.75f;

            private void Start()
            {
                //Get the line and reel fish components
                m_line = GetComponent<LineRenderer>();
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
                        DrawFishingLine();
                        break;
                    case state.PreCast:
                        //Just draws a line along the rod to the bobber
                        List<Vector3> points = new List<Vector3>(3);
                        m_line.positionCount = 3;
                        points.Add(transform.position + transform.forward * -0.05f);
                        points.Add(m_rodTip.position + m_rodTip.forward * -0.05f + m_rodTip.up * 0.1f);
                        points.Add(m_bobber.transform.position);
                        m_line.SetPositions(points.ToArray());
                        break;
                    case state.Casting:
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
                        m_bobber.transform.position = Vector3.Lerp(new Vector3(m_bobber.transform.position.x, GameSettings.POOL_HEIGHT, m_bobber.transform.position.z), new Vector3(m_rodTip.position.x, GameSettings.POOL_HEIGHT, m_rodTip.position.z), Time.deltaTime);
                        //Return to the cast state when no motion is inputed
                        if (m_reelVelocity >= 0)
                        {
                            m_rodState = state.Cast;
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
            public void MountBobber()
            {
                //pull out the hook
                //m_reelFish.SurfaceFish();
                m_bobber.transform.SetParent(m_bobberHold);
                m_bobber.GetComponent<Rigidbody>().isKinematic = true;
                m_bobber.transform.position = m_bobberHold.position;
                m_rodState = state.PreCast;
            }
            /// <summary>
            /// Upon getting near to the player/pier, pull the fish from the water.
            /// </summary>
            public void SurfaceFish(Fish fish)
            {

                //Instance the fish model as a child of the empty parent
                GameObject caughtFish = Instantiate(fish.data.model, m_fishDisplayPoint);
                //Its a grabbale kinematic rigidbody, so add the components
                Rigidbody fishRb = caughtFish.AddComponent<Rigidbody>();
                XRGrabInteractable fishGrab = caughtFish.AddComponent<XRGrabInteractable>();
                fishRb.isKinematic = true;
                fishGrab.useDynamicAttach = true;
                FindObjectOfType<RodController>().MountBobber();
            }
        }
    }
}
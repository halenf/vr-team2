using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using FishingGame.Objects;
using UnityEngine.Events;
using FishingGame.Checklist;
using FishingGame.UI;
using Unity.VisualScripting;

namespace FishingGame
{
    namespace FishingRod
    {
        [RequireComponent(typeof(LineRenderer))]
        //Controls player interactions with the fishing rod
        public class RodController : MonoBehaviour
        {
            //States
            public enum RodState
            {
                Cast,
                Mounted,
                Casting,
                Reeling
            }
            private RodState m_rodState = 0;
            public RodState rodState
            {
                get { return m_rodState; }
                set { m_rodState = value; }
            }

            //Casting
            [Tooltip("The scale of the force applied to the bobber when casting.")]
            [SerializeField] private float m_castForceScale;

            //Reeling
            private float m_reelVelocity;
            public float setReelVelo { set { m_reelVelocity = Mathf.Clamp(value, -1.0f, 0.0f); } }
            [Tooltip("The speed which the bobber is reeled in by.")]
            [SerializeField] private float m_reelForceScale = 1;
            public float getReelForce { get { return m_reelVelocity * m_reelForceScale; } }

            //Fishing line visuals
            private LineRenderer m_line;
            private float m_lineTension = 0.75f;
            [Header("Fishing Line Visuals")]
            [Tooltip("The points along the rod that the fishing line should go through.")]
            [SerializeField] private Transform[] m_linePoints;
            private GameObject m_fishInstance;

            //Hook
            [Header("Bobber")]
            [SerializeField] private Bobber m_bobber;
            private Transform m_bobberHold;
            [SerializeField] private Transform m_rodTip;
            public Transform getTip { get { return m_rodTip; } }
            [Tooltip("The transform to mount the fish at.")]
            [SerializeField] private Transform m_fishDisplayPoint;

            //Grabbing Visuals
            [Header("Grabbing Visuals")]
            [SerializeField] private XRInteractorLineVisual m_lineVisual;
            private Gradient m_invalidGradient;

            //Checklist
            private FishChecklist m_cList;

            // for animations/sounds
            public bool lineIsBeingMoved { get { return m_rodState == RodState.Reeling || m_bobber.hasHookedAgent; } }

            private void Start()
            {
                //Get the line components for visuals
                m_line = GetComponent<LineRenderer>();
                //Get the parent of the bobber, this is used for mounting the bobber
                m_bobberHold = m_bobber.transform.parent;
                //Get the fish checklist
                m_cList = FindObjectOfType<FishChecklist>();
                //Get the controller line renderer invalid gradient
                m_invalidGradient = new Gradient();
                m_invalidGradient.SetKeys(
                    m_lineVisual.invalidColorGradient.colorKeys,
                    m_lineVisual.invalidColorGradient.alphaKeys);
                //mount it for safety
                MountBobber();
                //Disable the current gradient
                m_lineVisual.invalidColorGradient.SetKeys(
                    new GradientColorKey[] { new GradientColorKey(Color.black, 0.0f) },
                    new GradientAlphaKey[] { new GradientAlphaKey(0, 0.0f) });
            }

            void Update()
            {
                if (m_fishInstance == null)
                    //Disable the current gradient
                    m_lineVisual.invalidColorGradient.SetKeys(
                        new GradientColorKey[] { new GradientColorKey(Color.black, 0.0f) },
                        new GradientAlphaKey[] { new GradientAlphaKey(0, 0.0f) });
                switch (m_rodState)
                {
                    case RodState.Cast:
                        //Set the line tension to a regular amount
                        m_lineTension = 0.75f;
                        //Check for any imput on the control stick
                        if (m_reelVelocity < 0)
                        {
                            m_rodState = RodState.Reeling;
                        }
                        break;
                    case RodState.Casting:
                        if (m_fishInstance)
                        {
                            m_rodState = RodState.Mounted;
                            return;
                        }
                        m_lineVisual.invalidColorGradient.SetKeys(
                            new GradientColorKey[] { new GradientColorKey(Color.black, 0.0f) },
                            new GradientAlphaKey[] { new GradientAlphaKey(0, 0.0f) });

                        m_bobber.GetComponent<Rigidbody>().isKinematic = false;
                        //Apply the controller's velocity to it
                        m_bobber.GetComponent<Rigidbody>().AddForce(m_bobber.transform.parent.GetComponent<Rigidbody>().velocity * m_castForceScale, ForceMode.VelocityChange);
                        //The rod has now been cast
                        //Unparent the bobber from its mount
                        m_bobber.transform.SetParent(null);
                        m_bobberHold.GetComponent<Rigidbody>().isKinematic = true;
                        m_bobberHold.position = m_rodTip.position;
                        m_rodState = RodState.Cast;
                        break;
                    case RodState.Reeling:
                        //Pull the bobber towards the player
                        //m_bobber.transform.position = Vector3.Lerp(new Vector3(m_bobber.transform.position.x, GameSettings.POOL_HEIGHT, m_bobber.transform.position.z), new Vector3(m_rodTip.position.x, GameSettings.POOL_HEIGHT, m_rodTip.position.z), Time.deltaTime);
                        //Return to the cast state when no motion is inputed
                        if (m_reelVelocity >= 0)
                        {
                            m_rodState = RodState.Cast;
                        }
                        break;

                }
                DrawFishingLine();
            }

            /// <summary>
            /// Finds the mid point between 2 points, and lowers it slightly.
            /// </summary>
            private Vector3 FindLineHalfPoint(Vector3 initPos, Vector3 targPos)
            {
                //find the mid point stuff;
                float yDiff = initPos.y - targPos.y;
                Vector3 pos = (initPos + targPos) / 2.0f;
                pos.y = initPos.y - yDiff * m_lineTension;

                return pos;
            }

            /// <summary>
            /// Draws the line when cast out
            /// </summary>
            private void DrawFishingLine()
            {
                List<Vector3> points = new List<Vector3>();
                //Draw the fishing line from the base of the rod to the tip
                foreach (Transform point in m_linePoints)
                {
                    points.Add(point.position);
                }
                //Add the tip to the list
                //points.Add(m_rodTip.position);

                // if the rod is out, draw a line from the tip to the bobber
                if (rodState != RodState.Mounted)
                {
                    m_lineTension = 0.75f - 0.25f * -m_reelVelocity;
                    Vector3[] segs = new Vector3[3];
                    //mid
                    segs[1] = FindLineHalfPoint(m_rodTip.position, m_bobber.transform.position);
                    //close
                    segs[0] = FindLineHalfPoint(m_rodTip.position, segs[1]);
                    //far
                    segs[2] = FindLineHalfPoint(segs[1], m_bobber.transform.position);

                    points.AddRange(segs);
                }

                //Then from the tip to the bobber
                points.Add(m_bobber.transform.position);
                //attach the points
                m_line.positionCount = points.Count;
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
                m_bobber.transform.localPosition = Vector3.zero;
                m_bobberHold.GetComponent<Rigidbody>().isKinematic = false;
                m_rodState = RodState.Mounted;
                m_lineVisual.invalidColorGradient.SetKeys(m_invalidGradient.colorKeys, m_invalidGradient.alphaKeys);
            }

            /// <summary>
            /// Upon getting near to the player/pier, pull the fish from the water.
            /// </summary>
            public void SurfaceFish(Fish fish)
            {
                MountBobber();
                //Instance the fish model as a child of the empty parent
                m_fishInstance = Instantiate(fish.data.model);
                AttachObjectToDisplayPoint(m_fishInstance.transform);

                if (m_cList.UnlockEntry(fish.data.speciesName))
                {
                    // send out an update that a new fish was found
                }
                if (m_cList.SetEntryRecordLength(fish.data.speciesName, fish.length))
                {
                    // send out an update that a bigger fish was found
                }
                if (m_cList.SetEntryRecordWeight(fish.data.speciesName, fish.weight))
                {
                    // send out an update that a heavier fish was found
                }
                if(FindObjectOfType<FishChecklistUI>())
                    FindObjectOfType<FishChecklistUI>().UpdateUI();

                //Its a grabbale kinematic rigidbody, so add the components
                /*Rigidbody fishRb = caughtFish.AddComponent<Rigidbody>();
                //fishRb.isKinematic = true;
                fishRb.useGravity = false;*/
                /*XRGrabInteractable fishGrab = caughtFish.AddComponent<XRGrabInteractable>();
                fishGrab.useDynamicAttach = true;
                fishGrab.retainTransformParent = false;
                fishGrab.forceGravityOnDetach = true;*/
                /*SpringJoint fishJoint = caughtFish.AddComponent<SpringJoint>();
                fishJoint.autoConfigureConnectedAnchor = false;
                fishJoint.connectedAnchor = m_fishDisplayPoint.position;
                fishJoint.spring = 10000.0f;
                fishJoint.damper = 100.0f;
                fishJoint.minDistance = 0;
                fishJoint.maxDistance = 1;*/
            }
            public void AttachObjectToDisplayPoint(Transform obj)
            {
                obj.position = m_bobber.transform.position;
                try
                {
                    obj.GetComponent<Joint>().connectedBody = m_bobber.GetComponent<Rigidbody>();
                }
                catch (Exception e)
                {
                    SpringJoint sJoint = obj.AddComponent<SpringJoint>();
                    sJoint.spring = 10000;
                    sJoint.connectedBody = m_bobber.GetComponent<Rigidbody>();
                }
                obj.rotation = Quaternion.identity;
            }
        }
    }
}
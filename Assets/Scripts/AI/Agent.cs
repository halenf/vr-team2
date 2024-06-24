using FishingGame.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        [DisallowMultipleComponent]
        public class Agent : MonoBehaviour
        {
            // fish object
            private Fish m_fish;
            public Fish fish { get { return m_fish; } }

            // state machine
            [SerializeField] private FiniteStateMachine m_stateMachine;
            public FiniteStateMachine stateMachine { get { return m_stateMachine; } }

            // variables for AI
            private Vector3 m_targetPosition = new Vector3();
            public Vector3 targetPosition { get { return m_targetPosition; } }

            private BuoyantObject m_bobber;
            public Vector3 bobberPosition 
            { get { return m_bobber.transform.position; }
              set { m_bobber.transform.position = value; } }
            public bool bobberIsUnderwater { get { return m_bobber.isUnderwater; } }

            private Transform m_playerTransform;
            public Vector3 playerPosition { get { return m_playerTransform.position; } }

            // set variables from data
            private float m_swimWaitTime;
            public float swimWaitTime { get { return m_swimWaitTime; } }

            private float m_swimDetectionRange;
            public float swimDetectionRange { get { return m_swimDetectionRange; } }

            private float m_bobberWaitTime;
            public float bobberWaitTime { get { return m_bobberWaitTime; } }

            private float m_bobberDetectionRange;
            public float bobberDetectionRange { get { return m_bobberDetectionRange; } }

            public void Init(Fish fish, BuoyantObject bobber, Transform player)
            {
                m_fish = fish;
                m_bobber = bobber;
                m_playerTransform = player;

                SetAIVariables();
            }

            public void Start()
            {
                m_stateMachine.Enter(this);
            }

            public void Update()
            {
                State currentState = m_stateMachine.currentState;
                m_stateMachine.UpdateThis(this);
                if (m_stateMachine.currentState != currentState)
                    SetAIVariables();
            }

            private void OnDisable()
            {
                m_stateMachine.Exit(this);
            }

            public void SetTargetPosition(Vector3 position)
            {
                m_targetPosition = position;
            }

            private void SetAIVariables()
            {
                m_swimWaitTime = m_fish.GetConstraint(m_fish.data.swimWaitTime);
                m_swimDetectionRange = m_fish.GetConstraint(m_fish.data.swimDetectionRange);
                m_bobberWaitTime = m_fish.GetConstraint(m_fish.data.bobberWaitTime);
                m_bobberDetectionRange = m_fish.GetConstraint(m_fish.data.bobberDetectionRange);
            }

            public void MoveTowardsTarget(float speed)
            {
                transform.position += (m_targetPosition - transform.position).normalized * speed * Time.deltaTime;
            }

            public void PullBobberAwayFromTarget()
            {
                transform.position -= (m_targetPosition - bobberPosition).normalized * 1.4f * Time.deltaTime;
                bobberPosition = transform.position;
            }

            public void LookAtTarget()
            {
                transform.rotation = Quaternion.LookRotation(m_targetPosition - transform.position);
            }

            public void LookAwayFromTarget()
            {
                transform.rotation = Quaternion.LookRotation(transform.position - m_targetPosition);
            }

            private void OnDrawGizmos()
            {
                // draw target position
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(m_targetPosition, new Vector3(0.5f, 0.5f, 0.5f));
            }
        }
    }
}

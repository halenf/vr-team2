using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

            private Transform m_bobberTransform;
            public Vector3 bobberPosition 
            { get { return m_bobberTransform.position; }
              set { m_bobberTransform.position = value; } }

            private Transform m_playerTransform;
            public Vector3 playerPosition { get { return m_playerTransform.position; } }

            
            public void Awake()
            {
                m_fish = GetComponent<Fish>();
            }

            public void Start()
            {
                m_bobberTransform = GameObject.FindGameObjectWithTag("Bobber").transform;
                m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }

            public void OnEnable()
            {
                m_stateMachine.Enter(this);
            }

            public void Update()
            {
                m_stateMachine.UpdateThis(this);
            }

            private void OnDisable()
            {
                m_stateMachine.Exit(this);
            }

            public void SetTargetPosition(Vector3 position)
            {
                m_targetPosition = position;
            }

            public bool WithinRangeOfTarget(float range = 0)
            {
                return Vector2.Distance(transform.position, m_targetPosition) < range;
            }

            public void MoveTowardsTarget()
            {
                transform.position += (m_targetPosition - transform.position).normalized * 2f * Time.deltaTime;
            }
            public void MoveBobberAwayFromPlayer()
            {
                m_bobberTransform.position -= (m_playerTransform.position - m_bobberTransform.position).normalized * 1.4f * Time.deltaTime;
            }

            public void LookAtTarget()
            {
                transform.LookAt(m_targetPosition);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
            public void LookAwayFromTarget()
            {
                transform.LookAt(m_targetPosition);
                transform.rotation = Quaternion.Euler(0, -transform.rotation.eulerAngles.y, 0);
            }

            private void OnDrawGizmos()
            {
                // draw target position
                Handles.color = Color.green;
                Handles.DrawWireCube(m_targetPosition, new Vector3(0.5f, 0.5f, 0.5f));

                // draw bobber position
                if (m_bobberTransform != null && m_bobberTransform.position != m_targetPosition)
                {
                    Handles.color = Color.red;
                    Handles.DrawWireCube(bobberPosition, new Vector3(0.5f, 0.5f, 0.5f));
                }

                // draw player
                if (m_playerTransform != null && m_playerTransform.position != m_targetPosition)
                {
                    Handles.color = Color.blue;
                    Handles.DrawWireCube(m_playerTransform.position, new Vector3(0.6f, 2, 0.6f));
                }
            }
        }
    }
}

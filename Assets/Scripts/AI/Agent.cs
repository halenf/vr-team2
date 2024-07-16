using FishingGame.FishControls;
using FishingGame.FishingRod;
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

            // animator
            private FishAnimationController m_animationController;
            public FishAnimationController animationController { get { return m_animationController; } }

            // variables for AI
            private Vector3 m_targetPosition = new Vector3();
            public Vector3 targetPosition { get { return m_targetPosition; } }

            private Bobber m_bobber;
            public Bobber bobber { get { return m_bobber; } }
            public Vector3 bobberPosition 
            {
                get
                {
                    return new Vector3(m_bobber.transform.position.x, GameSettings.POOL_HEIGHT, m_bobber.transform.position.z);
                }
                set
                {
                    m_bobber.transform.position = value;
                }
            }

            private Transform m_playerTransform;
            public Vector3 playerPosition { get { return m_playerTransform.position; } }

            public void Init(Fish fish, Bobber bobber, Transform player)
            {
                m_fish = fish;
                m_bobber = bobber;
                m_playerTransform = player;
            }

            public void Start()
            {
                m_animationController = GetComponent<FishAnimationController>();
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

            public void MoveTowardsTarget(float speed)
            {
                transform.position += (m_targetPosition - transform.position).normalized * speed * Time.deltaTime;
            }

            public void PullBobberAwayFromTarget()
            {
                bobberPosition -= (m_targetPosition - bobberPosition).normalized * 1.4f * Time.deltaTime;
                transform.position = new Vector3(bobberPosition.x, GameSettings.POOL_HEIGHT, bobberPosition.z);
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
#if UNITY_EDITOR
                // draw target position
                Handles.color = Color.green;
                Handles.DrawWireCube(m_targetPosition, new Vector3(0.5f, 0.5f, 0.5f));
#endif
            }
        }
    }
}

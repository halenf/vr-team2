using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class FishStateMachine
    {
        private enum State
        {
            Swimming,
            Waiting,
            TargetingRod,
            Running
        }

        private State m_state;
        private Vector2 m_targetPosition;
        private float m_timer;
        
        public void Start()
        {
            m_state = State.Swimming;
        }

        public void Update(Fish fish)
        {
            switch (m_state)
            {
                case State.Swimming:
                    SwimmingState(fish);
                    break;
                case State.TargetingRod:
                    TargetingRodState(fish);
                    break;
                case State.Running:
                    RunningState(fish);
                    break;
            }
        }

        private void SwimmingState(Fish fish)
        {
            // if the fish does not have a target position
            if (m_targetPosition == Vector2.zero)
            {
                m_targetPosition = new Vector2 (fish.transform.position.x + fish.data.RandomConstraint(fish.data.swimRange), 
                    fish.transform.position.y + fish.data.RandomConstraint(fish.data.swimRange));
                m_timer = fish.data.RandomConstraint(fish.data.swimWaitTime);
            }

            // get the fish's current position
            Vector2 currentPosition = new Vector2(fish.transform.position.x, fish.transform.position.y);

            // if the fish is not within range of its target,
            if ((m_targetPosition - currentPosition).magnitude > 0.5f)
            {
                // move it towards that target
                fish.transform.position = Vector3.Lerp(fish.transform.position, m_targetPosition, fish.swimSpeed);
            }
            else
            {
                // set the timer
                m_timer = fish.data.RandomConstraint(fish.data.swimWaitTime);
            }

            if (m_timer < 0)
            {
                m_targetPosition = Vector2.zero;
            }

            if (m_timer > 0)
                m_timer -= Time.deltaTime;
        }

        private void TargetingRodState(Fish fish)
        {

        }

        private void RunningState(Fish fish)
        {

        }
    }
}

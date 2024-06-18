using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        [DisallowMultipleComponent]
        public class State : MonoBehaviour
        {
            [SerializeField] private Transform m_behaviourContainer;
            [SerializeField] private Transform m_transitionContainer;

            [Space]

            private List<Behaviour> m_behaviours;
            private List<Transition> m_transitions;

            public List<Behaviour> behaviours { get { return m_behaviours; } }
            public List<Transition> transitions { get { return m_transitions; } }

            private void Awake()
            {
                m_behaviours = new List<Behaviour>();
                m_behaviours = m_behaviourContainer.GetComponentsInChildren<Behaviour>().ToList();
                m_transitions = new List<Transition>();
                m_transitions = m_transitionContainer.GetComponentsInChildren<Transition>().ToList();
            }

            public void Enter(Agent agent)
            {
                foreach (Behaviour behaviour in m_behaviours)
                    behaviour.Enter(agent);
            }
            public void UpdateThis(Agent agent)
            {
                foreach (Behaviour behaviour in m_behaviours)
                    behaviour.UpdateThis(agent);
            }
            public void Exit(Agent agent)
            {
                foreach (Behaviour behaviour in m_behaviours)
                    behaviour.Exit(agent);
            }
        }
    }
}

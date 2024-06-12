using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public class State
        {
            public State(Behaviour behaviour, Transition transition)
            {
                m_behaviours = new List<Behaviour> { behaviour };
                m_transitions = new List<Transition> { transition };
            }
            public State(Behaviour[] behaviours, Transition transition)
            {
                m_behaviours = new List<Behaviour>();
                m_behaviours.AddRange(behaviours);
                m_transitions = new List<Transition> { transition };
            }
            public State(Behaviour behaviour, Transition[] transitions)
            {
                m_behaviours = new List<Behaviour> { behaviour };
                m_transitions = new List<Transition>();
                m_transitions.AddRange(transitions);
            }
            public State(Behaviour[] behaviours, Transition[] transitions)
            {
                m_behaviours = new List<Behaviour>();
                m_behaviours.AddRange(behaviours);
                m_transitions = new List<Transition>();
                m_transitions.AddRange(transitions);
            }

            private List<Behaviour> m_behaviours;
            private List<Transition> m_transitions;

            public List<Behaviour> behaviours { get { return m_behaviours; } }
            public List<Transition> transitions { get { return m_transitions; } }

            public void Enter(Agent agent)
            {
                foreach (Behaviour behaviour in m_behaviours)
                    behaviour.Enter(agent);
            }
            public void Update(Agent agent)
            {
                foreach (Behaviour behaviour in m_behaviours)
                    behaviour.Update(agent);
            }
            public void Exit(Agent agent)
            {
                foreach (Behaviour behaviour in m_behaviours)
                    behaviour.Exit(agent);
            }

            public void AddTransition(Condition condition, State targetState)
            {
                Transition transition = new Transition(condition, targetState);
                m_transitions.Add(transition);
            }
            public void AddBehaviour(Behaviour behaviour)
            {
                m_behaviours.Add(behaviour);
            }
        }
    }
}

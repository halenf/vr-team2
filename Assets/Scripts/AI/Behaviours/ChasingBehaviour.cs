using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        [Serializable]
        public class ChasingBehaviour : Behaviour
        {
            [SerializeField] private GameObject m_targetObject;

            public override void Enter(Agent agent)
            {
                
            }
        }
    }
}

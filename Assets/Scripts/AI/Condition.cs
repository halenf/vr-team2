using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        public abstract class Condition : MonoBehaviour
        {
            [SerializeField] protected enum TargetValueType
            {
                Value,
                FishTarget,
                Bobber,
                Spooked
            }

            public virtual void Enter(Agent agent) { }
            public abstract bool IsTrue(Agent agent);
        }        
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace AI
    {
        [DisallowMultipleComponent]
        public abstract class Behaviour : MonoBehaviour
        {
            public virtual void Enter(Agent agent) { }
            public virtual void UpdateThis(Agent agent) { }
            public virtual void Exit(Agent agent) { }
        }
    }
}

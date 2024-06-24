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
            public abstract bool IsTrue(Agent agent);
        }        
    }
}
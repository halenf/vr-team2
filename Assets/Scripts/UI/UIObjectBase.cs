using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FishingGame
{
    namespace UI
    {
        public abstract class UIObjectBase<T> : MonoBehaviour
        {
            protected T m_baseObject;

            public abstract void UpdateUI();
        }
    }
}

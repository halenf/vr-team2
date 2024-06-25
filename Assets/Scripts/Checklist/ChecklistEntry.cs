using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace Checklist
    {
        public class ChecklistEntry
        {
            private bool m_isUnlocked;
            private string m_speciesName;
            private float m_recordWeight;
            private float m_recordLength;

            public bool isUnlocked { get { return m_isUnlocked; } }
            public string speciesName { get { return m_speciesName; } }
            public float recordWeight { get { return m_recordWeight; } }
            public float recordLength { get { return m_recordLength; } }

            public ChecklistEntry(FishData fishData)
            {
                m_isUnlocked = false;
                m_speciesName = fishData.speciesName;
                m_recordWeight = 0;
                m_recordLength = 0;
            }
        }
    }
}

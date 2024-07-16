using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    namespace Checklist
    {
        [System.Serializable]
        public class ChecklistEntry
        {
            [SerializeField] private string m_speciesName;
            [SerializeField] private string m_scientificName;
            [SerializeField] private string m_aboutDetails;
            [SerializeField] private bool m_isUnlocked;
            [SerializeField] private float m_recordWeight;
            [SerializeField] private float m_recordLength;
            [SerializeField] private Sprite m_sprite;

            public bool isUnlocked { get { return m_isUnlocked; } }
            public string speciesName { get { return m_speciesName; } }
            public string scientificName { get { return m_scientificName; } }
            public string aboutDetails { get { return m_aboutDetails; } }
            public float recordWeight { get { return m_recordWeight; } }
            public float recordLength { get { return m_recordLength; } }
            public Sprite sprite { get { return m_sprite; } }

            public ChecklistEntry(FishData fishData)
            {
                m_isUnlocked = false;
                m_speciesName = fishData.speciesName;
                m_recordWeight = 0;
                m_recordLength = 0;
                m_sprite = fishData.sprite;
            }

            public bool UnlockFish()
            {
                if (!m_isUnlocked)
                {
                    m_isUnlocked = true;
                    return true;
                }
                return false;
            }

            public bool SetRecordWeight(float value)
            {
                if (value > m_recordWeight)
                {
                    m_recordWeight = value;
                    return true;
                }
                return false;
            }

            public bool SetRecordLength(float value)
            {
                if (value > m_recordLength)
                {
                    m_recordLength = value;
                    return true;
                }
                return false;
            }
        }
    }
}

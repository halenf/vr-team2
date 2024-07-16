using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace FishingGame
{
    using Checklist;

    namespace UI
    {
        public class ChecklistEntryUI : UIObjectBase<ChecklistEntry>
        {
            [SerializeField] private TMP_Text m_speciesNameDisplay;
            [SerializeField] private TMP_Text m_scientificNameDisplay;
            [SerializeField] private Image m_spriteDisplay;
            [SerializeField] private TMP_Text m_recordWeightDisplay;
            [SerializeField] private TMP_Text m_recordLengthDisplay;
            [SerializeField] private TMP_Text m_aboutFishDisplay;
            
            public void Init(ChecklistEntry checklistEntry)
            {
                m_baseObject = checklistEntry;
                UpdateUI();
            }

            public override void UpdateUI()
            {
                m_spriteDisplay.sprite = m_baseObject.sprite;
                if (m_baseObject.isUnlocked)
                {
                    m_speciesNameDisplay.text = m_baseObject.speciesName;
                    m_scientificNameDisplay.text = m_baseObject.scientificName;
                    m_aboutFishDisplay.text = m_baseObject.aboutDetails;
                    m_recordWeightDisplay.text = "Best Weight: " + m_baseObject.recordWeight.ToString();
                    m_recordLengthDisplay.text = "Best Length: " + m_baseObject.recordLength.ToString();
                    m_spriteDisplay.color = Color.white;
                }
                else
                {
                    m_speciesNameDisplay.text = "???";
                    m_scientificNameDisplay.text = "???";
                    m_aboutFishDisplay.text = "???";
                    m_recordWeightDisplay.text = "???";
                    m_recordLengthDisplay.text = "???";
                    m_spriteDisplay.sprite = m_baseObject.sprite;
                    m_spriteDisplay.color = Color.black;
                }
            }
        }
    }
}

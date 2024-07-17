using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FishingGame
{
    using Checklist;

    namespace UI
    {
        public class FishChecklistUI : UIObjectBase<FishChecklist>
        {
            private ChecklistEntryUI[] m_checklistEntryUIs;

            [Header("Checklist Entries")]
            [SerializeField] private ChecklistEntryUI m_checklistEntryPrefab;
            [SerializeField] private Transform m_entryContainer;

            private void Start()
            {
                Init(GameObject.FindWithTag("Player").GetComponent<FishChecklist>());


            }

            public void Init(FishChecklist fishChecklist)
            {
                m_baseObject = fishChecklist;

                m_checklistEntryUIs = new ChecklistEntryUI[m_baseObject.Count];

                for (int e = 0; e < m_checklistEntryUIs.Length; e++)
                {
                    m_checklistEntryUIs[e] = Instantiate(m_checklistEntryPrefab, m_entryContainer);
                    m_checklistEntryUIs[e].Init(m_baseObject.GetEntry(e));
                }

                UpdateUI();
            }
            
            public override void UpdateUI()
            {
                foreach (ChecklistEntryUI ui in m_checklistEntryUIs)
                {
                    ui.UpdateUI();
                }
            }
        }
    }
}

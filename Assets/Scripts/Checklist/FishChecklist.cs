using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FishingGame
{
    namespace Checklist
    {
        public class FishChecklist : MonoBehaviour
        {
            [SerializeField] private ChecklistEntry[] m_checklistEntries;

            [ContextMenu("Initialise Entries")]
            public void InitialiseChecklistEntries()
            {
#if UNITY_EDITOR
                // use the Agent Manager FishData loading method to populate m_checklistEntries with an entry for every FishData.
#endif
            }
        }
    }
}

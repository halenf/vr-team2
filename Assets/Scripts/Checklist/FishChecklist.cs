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
            [SerializeField] private bool m_startUnlocked = false;
            
            [SerializeField] private string m_fishDataPath = "Assets/FishData/";
            
            [SerializeField] private ChecklistEntry[] m_checklistEntries;

            public int Count { get { return m_checklistEntries.Length; } }

            public ChecklistEntry GetEntry(int index)
            {
                return m_checklistEntries[index];
            }
            
            public ChecklistEntry GetEntry(string speciesName)
            {
                return System.Array.Find(m_checklistEntries, entry => entry.speciesName == speciesName);
            }

            public bool UnlockEntry(string speciesName)
            {
                return GetEntry(speciesName).UnlockFish();
            }

            public bool SetEntryRecordWeight(string speciesName, float weight)
            {
                return GetEntry(speciesName).SetRecordWeight(weight);
            }

            public bool SetEntryRecordLength(string speciesName, float length)
            {
                return GetEntry(speciesName).SetRecordLength(length);
            }

            [ContextMenu("Initialise Entries")]
            public void InitialiseChecklistEntries()
            {
#if UNITY_EDITOR
                // use the Agent Manager FishData loading method to populate m_checklistEntries with an entry for every FishData.
                List<ChecklistEntry> checklistEntries = new List<ChecklistEntry>();
                string[] guids = AssetDatabase.FindAssets($"t:{typeof(FishData).Name}", new string[] { m_fishDataPath }); //, new string[] { m_fishDataPath }
                foreach (string guid in guids)
                {
                    checklistEntries.Add(new ChecklistEntry(AssetDatabase.LoadAssetAtPath<FishData>(AssetDatabase.GUIDToAssetPath(guid))));
                }

                m_checklistEntries = checklistEntries.ToArray();

                if (m_checklistEntries.Length > 0)
                    Debug.Log($"Successfully populated the {name} Checklist with all FishDatas at {m_fishDataPath}.");
#endif
            }

            private void Start()
            {
                if (m_startUnlocked)
                    foreach (ChecklistEntry entry in m_checklistEntries)
                        entry.UnlockFish();
            }
        }
    }
}

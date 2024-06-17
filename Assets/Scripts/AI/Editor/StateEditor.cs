using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FishingGame
{
    using AI;
    using System;
    using System.Linq;
    using System.Reflection;
    using UnityEngine.UIElements;

    [CustomEditor(typeof(State))]
    public class StateEditor : CustomEditorBase
    {
        SerializedProperty m_behaviours, m_transitions;

        private List<Type> m_behaviourTypes;
        private int m_choice;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_behaviourTypes = Assembly.GetAssembly(typeof(Behaviour)).GetTypes().
                Where(myType => myType != typeof(FiniteStateMachine) && myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Behaviour))).ToList();
        }

        protected override void OnGUI()
        {
            EditorGUILayout.LabelField("Behaviours", EditorStyles.boldLabel);
            m_choice = EditorGUILayout.Popup(m_choice, m_behaviourTypes.Select(type => type.Name).ToArray());
            if (GUILayout.Button("+"))
            {
                string mainAssetPath = AssetDatabase.GetAssetPath(target);
                List<string> objs = mainAssetPath.Split("/").ToList();
                if (objs.Count > 0)
                {
                    objs.RemoveAt(objs.Count - 1);
                    mainAssetPath = string.Join("/", objs);
                }
                CreateInstanceOfScriptableObject(m_behaviourTypes[m_choice], m_behaviours, mainAssetPath);
            }
            if (m_behaviours.arraySize != 0 && GUILayout.Button("-"))
            {
                ((State)target).RemoveBehaviour();
                EditorUtility.SetDirty(target);
            }
            EditorGUI.indentLevel++;
            for (int b = 0; b < m_behaviours.arraySize; b++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Element " + (b + 1));
                EditorGUILayout.LabelField(m_behaviours.GetArrayElementAtIndex(m_choice).name);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;
            

            //EditorGUILayout.PropertyField(m_behaviours);
            EditorGUILayout.PropertyField(m_transitions);
        }

        protected override void GetSerializedProperties()
        {
            m_behaviours = serializedObject.FindProperty("m_behaviours");
            m_transitions = serializedObject.FindProperty("m_transitions");
        }
    }
}

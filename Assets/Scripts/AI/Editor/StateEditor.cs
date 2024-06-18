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

        // behaviours
        private List<Type> m_behaviourTypes;
        private int m_choice;

        // condition
        private List<Type> m_conditionTypes;
        private Condition m_craftingCondition;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_behaviourTypes = Assembly.GetAssembly(typeof(Behaviour)).GetTypes().
                Where(myType => myType != typeof(FiniteStateMachine) && myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Behaviour))).ToList();
        }

        protected override void OnGUI()
        {
            EditorGUILayout.LabelField("Behaviours", EditorStyles.boldLabel);

            // dropdown for choosing the type of behaviour to add to the array
            m_choice = EditorGUILayout.Popup(m_choice, m_behaviourTypes.Select(type => type.Name).ToArray());

            // add behaviour button
            // adds the behaviour currently selected in the dropdown to m_behaviours
            if (GUILayout.Button("+"))
            {
                // check the State does not already contain this kind of behaviour
                for (int b = 0; b < m_behaviours.arraySize; b++)
                {
                    if (m_behaviours.GetArrayElementAtIndex(b).objectReferenceValue.name == m_behaviourTypes[m_choice].Name)
                    {
                        Debug.LogError(target.name + " already contains a " + m_behaviourTypes[m_choice].Name + " Behaviour!");
                        goto DoNotCreate;
                    }
                }

                // get the asset path of the target object (the State)
                string mainAssetPath = AssetDatabase.GetAssetPath(target);
                List<string> objs = mainAssetPath.Split("/").ToList();
                if (objs.Count > 0)
                {
                    objs.RemoveAt(objs.Count - 1);
                    mainAssetPath = string.Join("/", objs);
                }

                // Create the Behaviour instance in a folder called Behaviours in the same directory as the State
                CreateInstanceOfScriptableObject(m_behaviourTypes[m_choice], m_behaviours, mainAssetPath, "Behaviours");
                EditorUtility.SetDirty(target);
            }
            DoNotCreate:

            // if the array is 0, do not create the - button
            // removes a behaviour from the end of the array
            if (m_behaviours.arraySize != 0 && GUILayout.Button("-"))
            {
                ((State)target).RemoveBehaviour();
                EditorUtility.SetDirty(target);
            }

            // display the contents of m_behaviours
            EditorGUI.indentLevel++;
            for (int b = 0; b < m_behaviours.arraySize; b++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Element " + (b + 1));
                UnityEngine.Object obj = m_behaviours.GetArrayElementAtIndex(b).objectReferenceValue;
                EditorGUILayout.LabelField(obj.name);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.indentLevel--;
            
            // Condition creator tool (same as Behaviour creator)


            // show the transitions list
            EditorGUILayout.PropertyField(m_transitions);
        }

        protected override void GetSerializedProperties()
        {
            m_behaviours = serializedObject.FindProperty("m_behaviours");
            m_transitions = serializedObject.FindProperty("m_transitions");
        }
    }
}

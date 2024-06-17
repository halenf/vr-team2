using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FishingGame
{
    using AI;

    //[CustomPropertyDrawer(typeof(Behaviour))]
    public class BehaviourPropertyDrawer : PropertyDrawer
    {
        private int m_choice = -1;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();

            Type[] behaviourChildren = Assembly.GetAssembly(typeof(Behaviour)).GetTypes().
                Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Behaviour))).ToArray();

            DropdownField typeDropdown = new DropdownField("Behaviour", behaviourChildren.Select(type => type.Name).ToList(), 0);

            container.Add(typeDropdown);

            int choice = typeDropdown.index;
            if (choice != m_choice)
            {
                m_choice = choice;
                Behaviour instance = (Behaviour)Activator.CreateInstance(behaviourChildren[m_choice]);
                fieldInfo.SetValue(property.serializedObject.targetObject, instance);
            }

            return container;
        }
    }
}

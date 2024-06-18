using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

namespace FishingGame
{
    public abstract class CustomEditorBase : Editor
    {
        protected virtual void OnEnable()
        {
            GetSerializedProperties();
        }

        protected abstract void OnGUI();
        protected abstract void GetSerializedProperties();

        public sealed override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            
            OnGUI();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
                serializedObject.ApplyModifiedProperties();
            }
        }

        protected void CreateInstanceOfScriptableObject(Type type, SerializedProperty listProperty, string mainAssetPath, string folderName = "")
        {
            ScriptableObject obj = CreateInstance(type);
            ScriptableObject loadedObject;
            if (AssetDatabase.Contains(obj) && obj.GetType() == type)
            {
                loadedObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GetAssetPath(obj));
            }
            else
            {
                obj.name = type.Name;
                string path = "";
                if (folderName == string.Empty)
                    path = $"{mainAssetPath}/{obj.name}.asset";
                else
                    path = $"{mainAssetPath}/{folderName}/{obj.name}.asset";

                AssetDatabase.CreateAsset(obj, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                loadedObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            }
            listProperty.arraySize++;
            listProperty.GetArrayElementAtIndex(listProperty.arraySize - 1).objectReferenceValue = loadedObject;

            listProperty.serializedObject.ApplyModifiedProperties();

            EditorGUIUtility.PingObject(loadedObject);
        }
    }
}


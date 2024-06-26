using UnityEditor;
using UnityEngine;

namespace OccaSoftware.SCSS.Editor
{

    public class SCSSShaderGUI : ShaderGUI
    {
        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            Material t = materialEditor.target as Material;
            
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("Base Color Settings", EditorStyles.boldLabel);
            Color baseColor = EditorGUILayout.ColorField(new GUIContent("Base Color"), t.GetColor(ShaderParams._BaseColor), false, false, false);
            Texture2D baseColorTexture = (Texture2D)EditorGUILayout.ObjectField("Texture (Optional)", t.GetTexture(ShaderParams._BaseColorTexture), typeof(Texture2D), true, GUILayout.Height(EditorGUIUtility.singleLineHeight));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Normal Map Settings", EditorStyles.boldLabel);
            Texture2D normalTexture = (Texture2D)EditorGUILayout.ObjectField("Normal Texture (Optional)", t.GetTexture(ShaderParams._NormalTexture), typeof(Texture2D), true, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            float normalStrength = t.GetFloat(ShaderParams._NormalStrength);
            if(normalTexture != null)
                normalStrength = EditorGUILayout.FloatField("Normal Strength", normalStrength);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Highlight Area Settings", EditorStyles.boldLabel);
            Color highlightColor = t.GetColor(ShaderParams._HighlightColor);
            float highlightArea = EditorGUILayout.Slider("Highlight Area", t.GetFloat(ShaderParams._HighlightArea), 0f, 1f);
            if (highlightArea > 0)
            {
                highlightColor = EditorGUILayout.ColorField(new GUIContent("Highlight Color"), highlightColor, true, false, false);
                highlightColor.a = EditorGUILayout.Slider("Highlight Strength", highlightColor.a, 0f, 1f);
            }

            


            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Rim Lighting Settings", EditorStyles.boldLabel);
            Color rimColor = t.GetColor(ShaderParams._RimColor);
            float rimArea = EditorGUILayout.Slider("Rim Area", t.GetFloat(ShaderParams._RimArea), 0f, 1f);
            if (rimArea > 0)
            {
                rimColor = EditorGUILayout.ColorField(new GUIContent("Rim Color"), rimColor, true, false, false);
                rimColor.a = EditorGUILayout.Slider("Rim Strength", rimColor.a, 0f, 1f);
            }
            

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Shadow Settings", EditorStyles.boldLabel);
            Color shadowColor = EditorGUILayout.ColorField(new GUIContent("Shadow Color"), t.GetColor(ShaderParams._ShadowColor), true, false, false);
            shadowColor.a = EditorGUILayout.Slider("Shadow Strength", shadowColor.a, 0f, 1f);

            if (EditorGUI.EndChangeCheck())
            {
                t.SetTexture(ShaderParams._BaseColorTexture, baseColorTexture);
                t.SetColor(ShaderParams._BaseColor, baseColor);
                t.SetTexture(ShaderParams._NormalTexture, normalTexture);
                t.SetFloat(ShaderParams._NormalStrength, Mathf.Max(normalStrength, 0.0f));
                t.SetColor(ShaderParams._HighlightColor, highlightColor);
                t.SetFloat(ShaderParams._HighlightArea, highlightArea);
                t.SetColor(ShaderParams._RimColor, rimColor);
                t.SetFloat(ShaderParams._RimArea, rimArea);
                t.SetColor(ShaderParams._ShadowColor, shadowColor);
            }
        }

        private static class ShaderParams
        {
            public static int _BaseColorTexture = Shader.PropertyToID("_BaseColorTexture");
            public static int _BaseColor = Shader.PropertyToID("_BaseColor");
            public static int _NormalTexture = Shader.PropertyToID("_NormalTexture");
            public static int _NormalStrength = Shader.PropertyToID("_NormalStrength");
            public static int _HighlightColor = Shader.PropertyToID("_HighlightColor");
            public static int _HighlightArea = Shader.PropertyToID("_HighlightArea");
            public static int _RimColor = Shader.PropertyToID("_RimColor");
            public static int _RimArea = Shader.PropertyToID("_RimArea");
            public static int _ShadowColor = Shader.PropertyToID("_ShadowColor");
        }
    }

    

}
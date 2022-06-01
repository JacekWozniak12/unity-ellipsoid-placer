using UnityEngine;
using UnityEditor;

namespace EllipsePlacer.Editor
{
    public class CreatorPropertyHandler : IDisplayGUI
    {
        internal SerializedObject serialized_object;
        internal SerializedProperty prop_StartingPosition;
        internal SerializedProperty prop_Method;
        internal SerializedProperty prop_Scale;
        internal SerializedProperty prop_Prefab;
        internal SerializedProperty prop_Height;
        internal SerializedProperty prop_Width;
        internal SerializedProperty prop_Between;
        internal SerializedProperty prop_Material;
        internal SerializedProperty prop_Color;
        internal SerializedProperty prop_Precision;

        public CreatorPropertyHandler(CreatorSettingsSO creatorSettingsSO)
        {
            SetCreatorSettings(creatorSettingsSO);
        }

        public void SetCreatorSettings(CreatorSettingsSO so)
        {
            serialized_object = new SerializedObject(so);
            GetProperties(serialized_object);
        }

        public void OnDisplayGUI() => DisplayProperties();
        public SerializedObject GetSerializedObject() => serialized_object;

        public void GetProperties(SerializedObject so)
        {
            prop_StartingPosition = so.FindProperty("StartingPosition");
            prop_Method = so.FindProperty("Method");
            prop_Prefab = so.FindProperty("Prefab");
            prop_Scale = so.FindProperty("Scale");
            prop_Height = so.FindProperty("Height");
            prop_Width = so.FindProperty("Width");
            prop_Between = so.FindProperty("Between");
            prop_Material = so.FindProperty("Material");
            prop_Color = so.FindProperty("Color");
            prop_Precision = so.FindProperty("Precision");
        }

        public void ApplyPropertiesFromWindowTo(CreatorSettingsSO asset)
        {
            CreatorSettingsSO temp = serialized_object.targetObject as CreatorSettingsSO;
            asset.StartingPosition = temp.StartingPosition;
            asset.Method = temp.Method;
            asset.Scale = temp.Scale;
            asset.Prefab = temp.Prefab;
            asset.Height = temp.Height;
            asset.Width = temp.Width;
            asset.Material = temp.Material;
            asset.Color = temp.Color;
            asset.Between = temp.Between;
        }

        internal void DisplayProperties()
        {
            serialized_object.Update();

            EditorGUILayout.PropertyField(prop_StartingPosition);
            EditorGUILayout.PropertyField(prop_Method);
            EditorGUILayout.PropertyField(prop_Scale);
            EditorGUILayout.PropertyField(prop_Prefab);
            EditorGUILayout.PropertyField(prop_Material);
            EditorGUILayout.PropertyField(prop_Color);
            EditorGUILayout.PropertyField(prop_Height);
            EditorGUILayout.PropertyField(prop_Width);
            EditorGUILayout.PropertyField(prop_Between);
            EditorGUILayout.PropertyField(prop_Precision);

            serialized_object.ApplyModifiedProperties();
        }
    }
}
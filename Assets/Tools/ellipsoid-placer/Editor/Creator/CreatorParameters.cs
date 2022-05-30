namespace ItemPlacer.Editor
{
    using UnityEngine;
    using UnityEditor;

    public partial class Creator : EditorWindow
    {
        [SerializeField] internal CreatorSettingsSO currentSettings = default;
        internal SerializedObject serialized_object;

        internal void GetProperties(SerializedObject so)
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

        internal void ApplyPropertiesFromWindowTo(CreatorSettingsSO asset)
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
    }
}
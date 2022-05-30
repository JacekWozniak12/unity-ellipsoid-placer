namespace ItemPlacer.Editor
{
    using UnityEngine;
    using UnityEditor;

    public partial class Creator : EditorWindow
    {
        [MenuItem(Settings.WINDOW_MENU_NAME)]
        static void Init()
        {
            Creator window = EditorWindow.GetWindow(
                typeof(Creator),
                true,
                Settings.NAME) as Creator;

            window.minSize = Settings.WINDOW_SIZE;
            window.Show();
        }

        private void OnEnable()
        {
            if (currentSettings == null)
                currentSettings = ScriptableObject.CreateInstance<CreatorSettingsSO>();

            SetSerializedObject(currentSettings);
        }

        internal void SetSerializedObject(CreatorSettingsSO applied)
        {
            currentSettings = applied;
            serialized_object = new SerializedObject(currentSettings);
            GetProperties(serialized_object);
        }

        internal void ImportProcedure()
        {
            CreatorSettingsSO temp = Import();
            if (temp != null)
            {
                currentSettings = Instantiate(temp);
                SetSerializedObject(currentSettings);
            }
        }

        internal void ExportProcedure()
        {
            if (Export())
            {
                currentSettings = Instantiate(currentSettings);
                SetSerializedObject(currentSettings);
            }

        }

        internal void Place(CreatorSettingsSO settings)
        {
            GameObject parent = new GameObject("Group");
            parent.transform.position = settings.StartingPosition;

            GameObject temp = Instantiate(
                settings.Prefab,
                settings.StartingPosition,
                Quaternion.identity
                );

            UpdatePrefabVisuals(temp, settings);
            UpdatePrefabScale(temp, settings);
            PlaceInXZ(temp, parent, settings);
            DestroyImmediate(temp);
        }
    }
}
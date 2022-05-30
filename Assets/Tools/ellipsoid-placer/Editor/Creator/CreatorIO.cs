namespace ItemPlacer.Editor
{
    using UnityEngine;
    using UnityEditor;
    using System;

    public partial class Creator : EditorWindow
    {
        internal bool Export()
        {
            CreatorSettingsSO asset = ScriptableObject.CreateInstance<CreatorSettingsSO>();
            ApplyPropertiesFromWindowTo(asset);
            return ExportToFile(asset);
        }

        internal bool ExportToFile(CreatorSettingsSO asset)
        {
            try
            {
                string path = EditorUtility.SaveFilePanel(
                    "Save Parameters",
                    $"{Settings.FOLDER_STORAGE}",
                    $"{asset.GetName()}",
                    "asset");

                if (path.Length == 0) return false;
                else
                {
                    path = path.Replace(Application.dataPath, "Assets");
                    asset.EnableSaving();
                    AssetDatabase.CreateAsset(asset, path);
                    AssetDatabase.SaveAssets();
                    asset.DisableSaving();
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return false;
            }
        }

        internal CreatorSettingsSO Import()
        {
            try
            {
                string path = EditorUtility.OpenFilePanel(
                    "Load Parameters",
                    $"{Settings.FOLDER_STORAGE}",
                    "asset");

                if (path.Length == 0) return null;
                else
                {
                    path = path.Replace(Application.dataPath, "Assets");
                    return AssetDatabase.LoadAssetAtPath<CreatorSettingsSO>(path);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return null;
            }
        }
    }
}

using UnityEngine;
using UnityEditor;
using System;

namespace EllipsePlacer.Editor
{
    public class CreatorIO
    {
        public bool ExportToFile(CreatorSettingsSO asset)
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

        public CreatorSettingsSO ImportWithOpenFilePanel()
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

namespace ItemPlacer.Editor
{
    using UnityEngine;
    using UnityEditor;

    public partial class Creator : EditorWindow
    {
        internal void UpdatePrefabVisuals(GameObject temp, CreatorSettingsSO settings)
        {
            Renderer renderer = temp.GetComponent<Renderer>();
            if (settings.Material != null)
            {
                renderer.sharedMaterial = settings.Material;
                if (settings.Material.color != settings.Color)
                {
                    if (EditorUtility.DisplayDialog(
                        "Change color of the material",
                        "Do you want to change color of the material that you selected?",
                        "Yes",
                        "No")
                        )
                    {
                        settings.Material.color = settings.Color;
                    }
                }
            }
        }

        internal void UpdatePrefabScale(GameObject temp, CreatorSettingsSO settings)
        {
            Transform transform = temp.GetComponent<Transform>();
            transform.localScale *= settings.Scale;
        }
    }
}
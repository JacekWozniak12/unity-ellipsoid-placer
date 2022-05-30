using UnityEngine;

namespace EllipsePlacer
{
    public class CreatorSettingsSO : ScriptableObject
    {
        public Vector3 StartingPosition = Vector3.zero;

        [Header("Placement method")]
        [Space]
        public CreationMethod Method;

        [Header("Object placed")]
        [Space]
        public GameObject Prefab = default;
        public Material Material = default;
        public Color Color = Color.white;

        [Header("Ellipse")]
        [Space]
        [Range(1, 2)] public float Scale = 1;
        public float Height, Width = 1;
        [Range(0, 10)] public float Between = 1;
        [Range(1, 5)] public int Precision = 1;

        void OnEnable() => DisableSaving();
        public void EnableSaving() => hideFlags = HideFlags.NotEditable;
        public void DisableSaving() => hideFlags = HideFlags.DontSave;

        public string GetName()
        {
            string temp = "";

            if (Height == Width)
                temp += $"circle_r{Height}";
            else
                temp += $"ellipse_h{Height}_w{Width}";

            temp += $"_b{Between}";
            return temp;
        }
    }
}

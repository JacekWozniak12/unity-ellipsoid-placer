using UnityEngine;

namespace ItemPlacer
{
    public class CreatorSettingsSO : ScriptableObject
    {
        public void OnEnable() => DisableSaving();
        public void EnableSaving() => hideFlags = HideFlags.NotEditable;
        public void DisableSaving() => hideFlags = HideFlags.DontSave;

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
        public float Height = 1;
        public float Width = 1;
        [Range(0, 10)]
        public float Between = 1;
        [Range(1, 2)]
        public float Scale = 1;
        [Range(1, 5)]
        public int Precision = 1;

        public string GetName()
        {
            string temp = "";

            if (Height == Width)
            {
                temp += $"circle_r{Height}";
            }
            else
            {
                temp += $"ellipse_h{Height}_w{Width}";
            }
            temp += $"_b{Between}";
            return temp;
        }
    }
}

using System.Numerics;
namespace ItemPlacer
{
    using UnityEngine;
    class Positioner : MonoBehaviour
    {
        [SerializeField]
        PositionRotation[] CubePositions;

        [SerializeField]
        GameObject prefab;

        Vector3 size;
        Mesh mesh;

        private void UpdatePrefab()
        {
            size = prefab.GetComponent<Collider>().bounds.size;
            mesh = prefab.GetComponent<Mesh>();
        }

        private void OnDrawGizmos()
        {
            foreach (PositionRotation positionRotation in CubePositions)
            {
                Gizmos.color = new Color(255, 255, 255, 1f);
                Gizmos.DrawCube(transform.position + positionRotation.position, Vector3.one);
            }
        }

        public void OverridePositions(PositionRotation[] newPositions) => CubePositions = newPositions;
    }
}

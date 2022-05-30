using System;
using UnityEngine;

namespace EllipsePlacer
{
    [Serializable]
    public struct PositionRotation
    {
        public Vector3 position;
        public Quaternion rotation;

        public PositionRotation(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }

}
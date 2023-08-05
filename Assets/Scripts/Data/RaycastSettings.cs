using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct RaycastSettings
    {
        public float Distance;
        public LayerMask LayerMask;
    }
}
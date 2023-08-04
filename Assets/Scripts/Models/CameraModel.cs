using System;
using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class CameraModel : IDisposable
    {
        public bool HasTarget { get; private set; }
        public float MovingRatio { get; private set; }
        public Vector3 StartPosition { get; private set; }
        public ICameraTarget Target { get; private set; }

        public CameraModel(float movingRatio)
        {
            MovingRatio = movingRatio;
        }

        public void SetHasTarget(bool value)
        {
            HasTarget = value;
        }

        public void SetStartPosition(Vector3 value)
        {
            StartPosition = value;
        }

        public void SetTarget(ICameraTarget target)
        {
            Target = target;
        }

        public void Dispose()
        {
            Target = null;
        }
    }
}
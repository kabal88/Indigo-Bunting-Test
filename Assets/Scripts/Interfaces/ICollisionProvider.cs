using System;
using UnityEngine;

namespace Interfaces
{
    public interface ICollisionProvider
    {
        public event Action<Collision> CollisionEnter;
        public event Action<Collision> CollisionExit;

        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;
    }
}
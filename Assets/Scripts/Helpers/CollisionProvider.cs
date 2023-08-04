using System;
using Interfaces;
using UnityEngine;

namespace Components
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider))]
    public class CollisionProvider : MonoBehaviour, ICollisionProvider
    {
        public event Action<Collision> CollisionEnter;
        public event Action<Collision> CollisionExit;

        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;

        private void OnCollisionEnter(Collision collision)
        {
            CollisionEnter?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            CollisionExit?.Invoke(collision);
        }

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit?.Invoke(other);
        }
    }
}
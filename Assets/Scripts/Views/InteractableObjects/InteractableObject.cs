using Interfaces;
using UnityEngine;

namespace Views
{
    public abstract class InteractableObject : MonoBehaviour
    {
        public abstract void Interact(IOwner owner = null, ITarget target = null);
    }
}
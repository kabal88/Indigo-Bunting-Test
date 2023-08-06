using UnityEngine;

namespace Models
{
    public class UnitModel
    {
        public bool IsAlive { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsInteractable { get; private set; }
        public Vector3 StartLocalPosition { get; private set; }

        public UnitModel(bool isAlive = true, bool isActive = true, bool isInteractable = true)
        {
            IsAlive = isAlive;
            IsActive = isActive;
            IsInteractable = isInteractable;
        }
        
        public void SetIsActive(bool isOn)
        {
            IsActive = isOn;
        }
        
        public void SetIsAlive(bool isOn)
        {
            IsAlive = isOn;
        }
        
        public void SetStartLocalPosition(Vector3 value)
        {
            StartLocalPosition = value;
        }
        
        public void SetIsInteractable(bool isOn)
        {
            IsInteractable = isOn;
        }
    }
}
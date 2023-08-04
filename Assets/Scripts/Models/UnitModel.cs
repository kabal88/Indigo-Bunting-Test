using UnityEngine;

namespace Models
{
    public class UnitModel
    {
        public bool IsAlive { get; private set; }
        public bool IsActive { get; private set; }
        public int CurrentNumber { get; private set; }
        
        public Vector3 StartLocalPosition { get; private set; }

        public UnitModel(int currentNumber, bool isAlive = true, bool isActive = true)
        {
            CurrentNumber = currentNumber;
            IsAlive = isAlive;
            IsActive = isActive;
        }

        public void SetCurrentNumber(int value)
        {
            CurrentNumber = value;
        }

        public void AddToCurrentNumber(int value)
        {
            CurrentNumber += value;
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
    }
}
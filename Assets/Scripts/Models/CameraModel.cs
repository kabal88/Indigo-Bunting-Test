using UnityEngine;

namespace Controllers
{
    public class CameraModel
    {
        public float ForwardSpeed { get; private set; }
        public bool CanMove{ get; private set; }
        public Vector3 StartPosition { get; private set; }
        public Vector3 StartUnitHolderLocalPosition { get; private set; }
        
        public Vector3 UnitHolderLocalPosition { get; private set; }
        public float LeftBorder { get; private set; }
        public float RightBorder { get; private set; }
        public float SideSpeed { get; private set; }

        public CameraModel(float forwardSpeed, float leftBorder, float rightBorder, float sideSpeed)
        {
            ForwardSpeed = forwardSpeed;
            LeftBorder = leftBorder;
            RightBorder = rightBorder;
            SideSpeed = sideSpeed;
        }

        public void SetCanMove(bool value)
        {
            CanMove = value;
        }

        public void SetStartPosition(Vector3 value)
        {
            StartPosition = value;
        }
        
        public void SetUnitHolderLocalPosition(Vector3 value)
        {
            UnitHolderLocalPosition = value;
        }
        
        public void SetStartUnitHolderLocalPosition(Vector3 value)
        {
            StartUnitHolderLocalPosition = value;
        }
    }
}
using Interfaces;
using UnityEngine;

namespace Controllers.UnitStates
{
    public class FallingState : UnitStateBase
    {
        private float _timeOut = 2f;
        private float _currentTimeOut;
        private float _minSpeedOfHip = 0.1f;
        
        public FallingState(IUnitContext unit) : base(unit)
        {
        }

        public override void HandleState(UnitStateBase newState)
        {
            switch (newState)
            {
                case DeadState deadState:
                    Unit.SetState(Unit.DeadState);
                    break;
                case StandUpState standUpState:
                    Unit.SetState(Unit.StandUpState);
                    break;
            }
        }

        public override void StartState()
        {
            Unit.View.SetRigidbodiesKinematic(false);
            Unit.View.SetAnimatorEnabled(false);
            Unit.Model.SetIsInteractable(false);
            Unit.View.FloorCollisionProvider.TriggerEnter += OnFloorCollisionEnter;
        }

        private void OnFloorCollisionEnter(Collider other)
        {
            CheckForLanding();
        }

        public override void UpdateLocal(float deltaTime)
        {
            if (_currentTimeOut< 0)
            {
                _currentTimeOut = _timeOut;
                CheckForLanding();
            }
            else
            {
                _currentTimeOut -= deltaTime;
            }
        }

        private void CheckForLanding()
        {
            if (Unit.View.SqrSpeedOfHip < _minSpeedOfHip)
            {
                HandleState(Unit.StandUpState);
            }
        }

        public override void EndState()
        {
            Unit.View.FloorCollisionProvider.TriggerEnter -= OnFloorCollisionEnter;
        }
    }
}
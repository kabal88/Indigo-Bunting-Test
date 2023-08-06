using Interfaces;
using UnityEngine;
using Views;
using Views.Tags;

namespace Controllers.UnitStates
{
    public class FallingState : UnitStateBase
    {
        private bool _floorTouched;
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
                case DeadState:
                    Unit.SetState(Unit.DeadState);
                    break;
                case StandUpState:
                    Unit.SetState(Unit.StandUpState);
                    break;
            }
        }

        public override void StartState()
        {
            Debug.Log("FallingState: StartState");
            Unit.View.SetRigidbodiesKinematic(false);
            Unit.View.SetAnimatorEnabled(false);
            Unit.Model.SetIsInteractable(false);
            Unit.View.TriggerEnter += OnTriggerEnter;
            Unit.View.CollisionEnter += OnCollisionEnter;
            _currentTimeOut = _timeOut;
            _floorTouched = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out InteractableObject interactable))
            {
                interactable.Interact(target:Unit.View);
            }

            if (other.TryGetComponent(out FloorTag _))
            {
                _floorTouched = true;
                CheckForLanding();
            }
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out InteractableObject interactable))
            {
                interactable.Interact(target:Unit.View);
            }
        }

        public override void UpdateLocal(float deltaTime)
        {
            if (!_floorTouched)
                return;
            
            if (_currentTimeOut < 0)
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
            Unit.View.TriggerEnter -= OnTriggerEnter;
            Debug.Log("FallingState: EndState");
        }
    }
}
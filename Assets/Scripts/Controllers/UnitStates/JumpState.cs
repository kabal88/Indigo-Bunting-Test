using Interfaces;
using UnityEngine;
using Views;

namespace Controllers.UnitStates
{
    public class JumpState : UnitStateBase
    {
        public JumpState(IUnitContext unit) : base(unit)
        {
        }

        public override void HandleState(UnitStateBase newState)
        {
            switch (newState)
            {
                case MovingState movingState:
                    Unit.SetState(movingState);
                    break;
                case DeadState deadState:
                    Unit.SetState(deadState);
                    break;
                case IdleState idleState:
                    Unit.SetState(idleState);
                    break;
            }
        }

        public override void StartState()
        {
            Unit.View.CollisionProvider.CollisionEnter += OnCollisionEnter;
            Unit.View.CollisionProvider.TriggerEnter += OnTriggerEnter;
            Unit.View.SetTrailActive(true);
        }

        public override void UpdateLocal(float deltaTime)
        {
            if (Unit.Model.CurrentNumber < 0)
            {
                Unit.HandleState(Unit.DeadState);
            }
        }
        
        private void OnTriggerEnter(Collider obj)
        {
            // if (obj.TryGetComponent(out InteractableObject interactableObject))
            // {
            //     interactableObject.Interact(null, Unit.Target);
            // }
        }

        private void OnCollisionEnter(Collision obj)
        {
            // if (obj.gameObject.TryGetComponent(out InteractableObject interactableObject))
            // {
            //     interactableObject.Interact(null, Unit.Target);
            // }
        }

        public override void EndState()
        {
            Unit.View.CollisionProvider.CollisionEnter -= OnCollisionEnter;
            Unit.View.CollisionProvider.TriggerEnter -= OnTriggerEnter;
            Unit.View.SetTrailActive(false);
        } 
    }
}
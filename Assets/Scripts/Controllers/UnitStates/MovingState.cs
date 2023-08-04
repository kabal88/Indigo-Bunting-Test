using Interfaces;
using UnityEngine;
using Views;

namespace Controllers.UnitStates
{
    public class MovingState : UnitStateBase
    {
        public MovingState(IUnitContext unit) : base(unit)
        {
        }

        public override void HandleState(UnitStateBase newState)
        {
            switch (newState)
            {
                case DeadState deadState:
                    Unit.SetState(deadState);
                    break;
                case IdleState idleState:
                    Unit.SetState(idleState);
                    break;
                case CrossFinishLineState crossFinishLineState:
                    Unit.SetState(crossFinishLineState);
                    break;
                case JumpState jumpState:
                    Unit.SetState(jumpState);
                    break;
                case FallingState fallingState:
                    Unit.SetState(fallingState);
                    break;
            }
        }

        public override void StartState()
        {
            Unit.View.CollisionProvider.CollisionEnter += OnCollisionEnter;
            Unit.View.CollisionProvider.TriggerEnter += OnTriggerEnter;
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
        }   

        public override void UpdateLocal(float deltaTime)
        {
            if (Unit.Model.CurrentNumber < 0)
            {
                Unit.HandleState(Unit.DeadState);
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            EndState();
        }
    }
}
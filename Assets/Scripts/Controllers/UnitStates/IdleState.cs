using Interfaces;
using UnityEngine;

namespace Controllers.UnitStates
{
    public class IdleState : UnitStateBase
    {
        public IdleState(IUnitContext unit) : base(unit)
        {
        }

        public override void HandleState(UnitStateBase newState)
        {
            switch (newState)
            {
                case FallingState:
                    Unit.SetState(Unit.FallingState);
                    break;
            }
        }

        public override void StartState()
        {
            Debug.Log("IdleState: StartState");
            Unit.View.SetRigidbodiesKinematic(true);
            Unit.View.SetAnimatorEnabled(true);
            Unit.Model.SetIsInteractable(true);
            Unit.View.SetAnimationState(AnimationStateIdentifierMap.Idle);
        }

        public override void UpdateLocal(float deltaTime)
        {
        }

        public override void EndState()
        {
            Unit.Model.SetIsInteractable(false);
            Debug.Log("IdleState: EndState");
        }
    }
}
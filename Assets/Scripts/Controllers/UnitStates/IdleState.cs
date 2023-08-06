using Interfaces;

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
                case FallingState state:
                    Unit.SetState(state);
                    break;
            }
        }

        public override void StartState()
        {
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
        }
    }
}
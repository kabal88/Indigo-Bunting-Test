using Interfaces;

namespace Controllers.UnitStates
{
    public class FallingState : UnitStateBase
    {
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
            }
        }

        public override void StartState()
        {
            Unit.View.SetRigidbodiesKinematic(false);
            Unit.View.SetAnimatorEnabled(false);
        }

        public override void UpdateLocal(float deltaTime)
        {
        }

        public override void EndState()
        {
        }
    }
}
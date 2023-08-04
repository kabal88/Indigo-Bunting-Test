using Interfaces;

namespace Controllers.UnitStates
{
    public class DeadState : UnitStateBase
    {
        public DeadState(IUnitContext unit) : base(unit)
        {
        }

        public override void HandleState(UnitStateBase newState)
        {
        }

        public override void StartState()
        {
            Unit.View.PlayDeadAnimation(OnDead);
            Unit.Model.SetIsAlive(false);
        }

        private void OnDead()
        {
            Unit.OnDead();
        }

        public override void UpdateLocal(float deltaTime)
        {
            
        }

        public override void EndState()
        {
        }
    }
}
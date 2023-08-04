using Interfaces;

namespace Controllers.UnitStates
{
    public class CrossFinishLineState : UnitStateBase
    {
        public CrossFinishLineState(IUnitContext unit) : base(unit)
        {
        }

        public override void HandleState(UnitStateBase newState)
        {
        }

        public override void StartState()
        {
            Unit.OnCrossFinishLine();
        }

        public override void UpdateLocal(float deltaTime)
        {
        }

        public override void EndState()
        {
        }
    }
}
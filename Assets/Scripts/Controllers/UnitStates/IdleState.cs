using System;
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
                case MovingState movingState:
                    Unit.SetState(movingState);
                    break;
            }
        }

        public override void StartState()
        {
        }

        public override void UpdateLocal(float deltaTime)
        {
        }

        public override void EndState()
        {
        }
    }
}
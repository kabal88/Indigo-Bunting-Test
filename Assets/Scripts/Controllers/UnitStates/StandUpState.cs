using System;
using Interfaces;

namespace Controllers.UnitStates
{
    public class StandUpState : UnitStateBase
    {
        public StandUpState(IUnitContext unit) : base(unit)
        {
        }

        public override void HandleState(UnitStateBase newState)
        {
            switch (newState)
            {
                case IdleState state:
                    Unit.SetState(state);
                    break;
            }
        }

        public override void StartState()
        {
            Unit.View.SetRigidbodiesKinematic(true);
            Unit.View.SetAnimatorEnabled(true);
            Unit.View.AnimationEvent += OnAnimationEvent;
            Unit.View.SetAnimationState(AnimationStateIdentifierMap.StandUp);
        }

        private void OnAnimationEvent(int id)
        {
            if (id == AnimationEventIdentifierMap.StandUpFinished)
            {
                HandleState(Unit.IdleState);
            }
        }

        public override void UpdateLocal(float deltaTime)
        {
        }

        public override void EndState()
        {
            Unit.View.AnimationEvent -= OnAnimationEvent;
        }
    }
}
using Controllers.UnitStates;
using Models;
using Views;

namespace Interfaces
{
    public interface IUnitContext
    {
        IdleState IdleState { get; }
        DeadState DeadState { get; }
        CrossFinishLineState CrossFinishLineState { get; }
        FallingState FallingState { get; }
        StandUpState StandUpState { get; }
        UnitModel Model { get; }
        UnitView View { get; }

        /// <summary>
        /// Method for import state directly.
        /// </summary>
        /// <param name="newState"></param>
        void SetState(UnitStateBase newState);
        
        /// <summary>
        /// Method for trying to set state. Result depends on current state transactions.
        /// </summary>
        /// <param name="newState"></param>
        void HandleState(UnitStateBase newState);

        void OnDead();
        void OnCrossFinishLine();
    }
}
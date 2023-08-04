using System;
using Interfaces;

namespace Controllers.UnitStates
{
    public abstract class UnitStateBase : IUpdatable, IDisposable
    {
        protected IUnitContext Unit;

        public bool IsAlive { get; protected set; }

        public UnitStateBase(IUnitContext unit)
        {
            Unit = unit;
        }

        public abstract void HandleState(UnitStateBase newState);
        public abstract void StartState();
        public abstract void UpdateLocal(float deltaTime);
        public abstract void EndState();
        
        public virtual void Dispose()
        {
            Unit = null;
        }
    }
}
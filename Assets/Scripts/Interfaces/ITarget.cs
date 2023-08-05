using UnityEngine;

namespace Interfaces
{
    public interface ITarget
    {
        bool IsAlive { get; }
    }
    
    public interface IHaveUnitContext
    {
        IUnitContext UnitContext { get; }
    }
}
using System.Collections.Generic;

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

    public interface ITargetWithBones : ITarget
    {
        IEnumerable<BoneTag> Bones { get; }
    }
    
    public interface IMoneyCollector
    {
        void AddMoney(int value);
    }
}
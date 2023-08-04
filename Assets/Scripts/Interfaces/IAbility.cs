namespace Interfaces
{
    public interface IAbility
    {
        void Execute(IOwner owner = null, ITarget target = null);
    }
}
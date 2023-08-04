namespace Interfaces
{
    public interface IFixUpdatable
    {
        bool IsAlive { get; }
        void FixedUpdateLocal();
    }
}
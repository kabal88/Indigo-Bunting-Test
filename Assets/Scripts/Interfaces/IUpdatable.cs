namespace Interfaces
{
    public interface IUpdatable
    {
        bool IsAlive { get; }
        void UpdateLocal(float deltaTime);
    }
}
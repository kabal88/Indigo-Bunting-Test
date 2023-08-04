namespace Interfaces
{
    public interface ITarget
    {
        int CurrentNumber { get; }
        void AddToCurrentNumber(int value);
        void Die();
    }
}
namespace Interfaces
{
    public interface IOwner
    {
        int CurrentNumber { get; }
        void Die();
    }
}
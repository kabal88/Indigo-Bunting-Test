using Data;

namespace Interfaces
{
    public interface IInputListener: IReactCommand<InputStartedCommand>,
        IReactCommand<InputCommand>,
        IReactCommand<InputEndedCommand>
    {
        bool IsAlive { get; }
    }
}
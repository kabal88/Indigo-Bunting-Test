using Models;

namespace Interfaces
{
    public interface IInputDescription :IDescription
    {
        InputActionsModel Model { get; }
    }
}
using Controllers;
using Data;

namespace Interfaces
{
    public interface IPlayerControllerDescription : IDescription
    {
        AbilitiesController AbilitiesController { get; }
        RaycastSettings RaycastSettings { get; }
    }
}
using Models;
using UnityEngine;

namespace Interfaces
{
    public interface IGameDescription : IDescription
    {
        GameModel Model { get; }
    }
}
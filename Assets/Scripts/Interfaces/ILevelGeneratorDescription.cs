using Models;
using UnityEngine;

namespace Interfaces
{
    public interface ILevelGeneratorDescription : IDescription
    {
        LevelGeneratorModel Model { get; }
        GameObject Prefab { get; }
    }
}
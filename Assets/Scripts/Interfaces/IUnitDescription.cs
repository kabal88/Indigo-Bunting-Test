using Models;
using UnityEngine;

namespace Interfaces
{
    public interface IUnitDescription : IDescription
    {
        UnitModel Model { get; }
        GameObject Prefab { get; }
    }
}
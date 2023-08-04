using Data;
using UnityEngine;

namespace Interfaces
{
    public interface ISpawnPoint
    {
        Transform Parent { get; }
        SpawnData Data { get; }
    }
}
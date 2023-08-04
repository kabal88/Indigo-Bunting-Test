using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;
using Views;

namespace Systems
{
    public class SpawnService : IRepository<ISpawnPoint>
    {
        private readonly List<ISpawnPoint> _spawnPoints= new();

        public void Init()
        {
            var points= GameObject.FindObjectsOfType<SpawnPointView>();
           foreach (var point in points)
           {
               point.Init();
               RegisterObject(point);
           }
        }
        
        public void RegisterObject(ISpawnPoint obj)
        {
            _spawnPoints.Add(obj);
        }

        public void UnRegisterObject(ISpawnPoint obj)
        {
            _spawnPoints.Remove(obj);
        }

        public IEnumerable<ISpawnPoint> GetObjectsByPredicate(Func<ISpawnPoint, bool> predicate)
        {
            return _spawnPoints.Where(predicate);
        }
    }
}
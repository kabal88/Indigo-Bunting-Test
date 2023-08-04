using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Services;
using Systems;
using UnityEngine;
using Views;

namespace Controllers
{
    public class LevelGenerator
    {
        public event Action LevelGenerationFinished;
        
        private LevelGeneratorModel _model;
        private LevelHolder _holder;

        public LevelGenerator(LevelGeneratorModel model, GameObject prefab)
        {
            _holder = MonoBehaviour.Instantiate(prefab).GetComponent<LevelHolder>();
            _model = model;
        }

        public void GenerateLevel(int index)
        {
            CleanLevel();
            
            LevelGenerationFinished?.Invoke();
        }


        private void CleanLevel()
        {
            foreach (var r in _model.CurrentSegments.ToArray())
                MonoBehaviour.Destroy(r);
            
            _model.CurrentSegments.Clear();
        }

        private void GenerateRoad()
        {
            var spawnPoint = ServiceLocator.Get<SpawnService>()
                .GetObjectsByPredicate(x => x.Data.Id == SpawnPointIdentifierMap.RoadSpawnPoint).First().Data;

            // var segments = config.Segments;
            // float roadLength = 0;
            //
            // for (int i = 0; i < segments.Length; i++)
            // {
            //     var pos = new Vector3(0, 0, (roadLength));
            //     pos += spawnPoint.Position;
            //     
            //     var segment = MonoBehaviour.Instantiate(segments[i].Prefab, pos, spawnPoint.Rotation);
            //     segment.transform.SetParent(_holder.transform);
            //
            //     if (segment.TryGetComponent(out RoadSegmentHolder holder))
            //     {
            //         _model.RoadSegmentHolders.Add(holder);
            //     }
            //     _model.CurrentSegments.Add(segment);
            //     
            //     roadLength += segments[i].Lenght;
            // }
        }
    }
}
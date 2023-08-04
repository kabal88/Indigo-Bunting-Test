using System;
using System.Linq;
using Models;
using Services;
using UnityEngine;
using Views;


namespace Controllers
{
    public class LevelGenerator
    {
        public event Action LevelGenerationFinished;

        private LevelGeneratorModel _model;

        public LevelGenerator(LevelGeneratorModel model)
        {
            _model = model;
        }

        public void GenerateLevel(int index)
        {
            CleanLevel();
            GenerateEnvironment(_model.GetLevelConfig(index));
            LevelGenerationFinished?.Invoke();
        }


        private void CleanLevel()
        {
            foreach (var r in _model.EnvironmentObjects)
                MonoBehaviour.Destroy(r);

            _model.EnvironmentObjects.Clear();
        }

        private void GenerateEnvironment(LevelConfig config)
        {
            var spawnPoint = ServiceLocator.Get<SpawnService>()
                .GetObjectsByPredicate(x => x.Data.Id == SpawnPointIdentifierMap.EnvironmentPoint).First().Data;

            var environment= MonoBehaviour.Instantiate(config.EnvironmentPrefab, spawnPoint.Position, spawnPoint.Rotation);

            var holder = environment.GetComponent<LevelHolder>();
            holder.Init();

            _model.EnvironmentObjects.Add(environment);
        }
    }
}
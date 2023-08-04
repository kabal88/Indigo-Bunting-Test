using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    public class LevelGeneratorModel
    {
        public List<GameObject> EnvironmentObjects = new();

        private int _numberOfLevelsForRepeating;
        private int _numberOfLevels;
        private Dictionary<int, LevelConfig> _configs;
        
        public LevelGeneratorModel(int numberOfLevelsForRepeating,LevelConfig[] roadConfigs)
        {
            _configs = new ();
            _numberOfLevelsForRepeating = numberOfLevelsForRepeating;
            _numberOfLevels = roadConfigs.Length;
            
            for (int i = 0; i < roadConfigs.Length; i++)
            {
                LevelConfig c = roadConfigs[i];
                _configs.Add(i, c);
            }
        }

        public LevelConfig GetLevelConfig(int index, bool useRepeating = true)
        {
            if (useRepeating)
            {
                index = GetResolvedIndex(index);
            }
            
            return _configs.TryGetValue(index, out var levelConfig) ? levelConfig : default;
        }
        
        private int GetResolvedIndex(int index)
        {
            var resolvedIndex = _numberOfLevels - _numberOfLevelsForRepeating + (index - _numberOfLevels) % _numberOfLevelsForRepeating;

            if (resolvedIndex < 0)
            {
                resolvedIndex = 0;
            }

            return resolvedIndex;
        }
    }
}
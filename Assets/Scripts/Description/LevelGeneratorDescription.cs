using System;
using System.Linq;
using Helpers;
using Identifier;
using Interfaces;
using Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Descriptions
{
    [Serializable]
    public class LevelGeneratorDescription : ILevelGeneratorDescription
    {
        [SerializeField] private LevelGeneratorIdentifier _id;
        [SerializeField] private int _numberOfLevelsForRepeating = 3;
        [SerializeField] private LevelConfig[] _levelConfigs;

        public int Id => _id.Id;

        public LevelGeneratorModel Model => new(_numberOfLevelsForRepeating, _levelConfigs);
        


#if UNITY_EDITOR
        [Button(SdfIconType.Stack, IconAlignment.LeftOfText)]
        public void SortByOrder()
        {
            if (_levelConfigs == null)
                return;
            
            var sorted = _levelConfigs.OrderBy(x => x.Order);
            _levelConfigs = sorted.ToArray();
        }

        [Button(SdfIconType.Search, IconAlignment.LeftOfText)]
        public void CollectConfigs()
        {
            var array = new SOProvider<LevelConfig>().GetCollection().ToArray();
            _levelConfigs = array;
        }
#endif
    }
}
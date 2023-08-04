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
        [SerializeField] private GameObject _prefab;

        public int Id => _id.Id;

        public LevelGeneratorModel Model => new(_numberOfLevelsForRepeating);

        public GameObject Prefab => _prefab;


#if UNITY_EDITOR
        [Button(SdfIconType.Stack, IconAlignment.LeftOfText)]
        public void SortByOrder()
        {
            // if (_roadConfigs == null)
            //     return;
            //
            // var sorted = _roadConfigs.OrderBy(x => x.Order);
            // _roadConfigs = sorted.ToArray();
        }

        [Button(SdfIconType.Search, IconAlignment.LeftOfText)]
        public void CollectConfigs()
        {
            // var array = new SOProvider<RoadConfig>().GetCollection().ToArray();
            // _roadConfigs = array;
        }
#endif
    }
}
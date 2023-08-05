using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Libraries
{
    [Serializable]
    public sealed class Library //TODO: make it auto generatable
    {
        [SerializeField] private List<Descriptions.Description> Descriptions = new();

        private Dictionary<int, IGameDescription> _gameDescriptions = new();
        private Dictionary<int, IUnitDescription> _unitDescriptions = new();
        private Dictionary<int, ILevelGeneratorDescription> _levelGeneratorDescriptions = new();
        private Dictionary<int, IInputDescription> _inputDescriptions = new();
        private Dictionary<int, ICameraDescription> _cameraDescriptions = new();
        private Dictionary<int, IPlayerControllerDescription> _playerControllerDescriptions = new();


        public void Init()
        {
            foreach (var description in Descriptions)
            {
                switch (description.GetDescription)
                {
                    case IGameDescription data:
                        _gameDescriptions.Add(description.GetDescription.Id, data);
                        break;
                    case IUnitDescription data:
                        _unitDescriptions.Add(description.GetDescription.Id, data);
                        break;
                    case ILevelGeneratorDescription data:
                        _levelGeneratorDescriptions.Add(description.GetDescription.Id, data);
                        break;
                    case IInputDescription data:
                        _inputDescriptions.Add(description.GetDescription.Id, data);
                        break;
                    case ICameraDescription data:
                        _cameraDescriptions.Add(description.GetDescription.Id, data);
                        break;
                    case IPlayerControllerDescription data:
                        _playerControllerDescriptions.Add(description.GetDescription.Id, data);
                        break;
                }
            }
        }

#if UNITY_EDITOR

        /// <summary>
        /// Work ONLY from Editor. Use after adding new Description to project. 
        /// </summary>
        [Button(ButtonSizes.Large), GUIColor(0.5f, 0.8f, 1f),
         PropertyTooltip("Click after adding new Description to project.")]
        public void CollectAllDescriptions()
        {
            Descriptions = new SOProvider<Descriptions.Description>().GetCollection().ToList();
        }

#endif
        public IGameDescription GetGameDescription(int id)
        {
            if (_gameDescriptions.TryGetValue(id, out var needed))
            {
                return needed;
            }

            throw new Exception($"Game description with id {id} not found");
        }

        public IUnitDescription GetUnitDescription(int id)
        {
            if (_unitDescriptions.TryGetValue(id, out var needed))
            {
                return needed;
            }

            throw new Exception($"Unit description with id {id} not found");
        }

        public ILevelGeneratorDescription GetLevelGeneratorDescription(int id)
        {
            if (_levelGeneratorDescriptions.TryGetValue(id, out var needed))
            {
                return needed;
            }

            throw new Exception($"LevelGenerator description with id {id} not found");
        }

        public IInputDescription GetInputDescription(int id)
        {
            if (_inputDescriptions.TryGetValue(id, out var needed))
            {
                return needed;
            }

            throw new Exception($"Input description with id {id} not found");
        }

        public ICameraDescription GetCameraDescription(int id)
        {
            if (_cameraDescriptions.TryGetValue(id, out var needed))
            {
                return needed;
            }

            throw new Exception($"Camera description with id {id} not found");
        }
        
        public IPlayerControllerDescription GetPlayerControllerDescription(int id)
        {
            if (_playerControllerDescriptions.TryGetValue(id, out var needed))
            {
                return needed;
            }

            throw new Exception($"PlayerController description with id {id} not found");
        }
    }
}
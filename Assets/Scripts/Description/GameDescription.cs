using System;
using Identifier;
using Interfaces;
using Models;
using UnityEngine;

namespace Descriptions
{
    [Serializable]
    public class GameDescription : IGameDescription
    {
        [SerializeField] private GameIdentifier _id;
        [SerializeField] private LevelGeneratorIdentifier _levelGeneratorId;
        [SerializeField] private InputDescriptionIdentifier _inputDescriptionId;
        [SerializeField] private CameraIdentifier _cameraId;
        [SerializeField] private UnitIdentifier _unitId;
        [SerializeField] private PlayerControllerIdentifier _playerControllerId;

        public int Id => _id.Id;
        public GameModel Model => new(_levelGeneratorId.Id,
            _inputDescriptionId.Id,
            _cameraId.Id,
            _unitId.Id,
            _playerControllerId.Id);
    }
}
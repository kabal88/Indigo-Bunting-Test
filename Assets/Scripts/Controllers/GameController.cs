using Interfaces;
using Libraries;
using Models;
using Services;
using System;
using System.Linq;
using Views;


namespace Controllers
{
    public class GameController : IUpdatable, IFixUpdatable, IDisposable
    {
        private GameModel _model;
        private PlayerModel _playerModel;
        private Library _library;
        private UpdateLocalService _updateLocalService;
        private FixUpdateLocalService _fixUpdateLocalService;
        private SpawnService _spawnService;
        private GameUIController _gameUIController;
        private LevelGenerator _levelGenerator;
        private InputListenerService _inputListenerService;
        private CameraController _cameraController;
        private UnitController _unitController;
        private PlayerController _playerController;


        public bool IsAlive { get; }

        public GameController(GameModel gameModel,
            Library library,
            GameUIView gameUi,
            CameraView cameraView)
        {
            IsAlive = true;
            _model = gameModel;
            _library = library;
            _gameUIController = new GameUIController(gameUi);
            _cameraController = new CameraController(cameraView,
                _library.GetCameraDescription(_model.CameraDescriptionId).Model);
            _playerModel = new PlayerModel();
        }

        public void Init()
        {
            InitServices();

            var levelGeneratorDescription = _library.GetLevelGeneratorDescription(_model.LevelGeneratorId);
            _levelGenerator = new LevelGenerator(levelGeneratorDescription.Model);
            _levelGenerator.LevelGenerationFinished += OnLevelGenerationFinished;

            _levelGenerator.GenerateLevel(_playerModel.Level);
            _unitController = CreateUnit();

            _cameraController.Init();
            _cameraController.SetTarget(_unitController);

            _updateLocalService.RegisterObject(_cameraController);

            _unitController.HandleState(_unitController.StandUpState);
            _cameraController.SetActive(true);
            _gameUIController.HideAllWindows();
            _gameUIController.SetLevel(_playerModel.Level);
            _gameUIController.SetMoney(_playerModel.Money);
            _gameUIController.RestartButtonClicked += Restart;
            
            var playerControllerDescription = _library.GetPlayerControllerDescription(_model.PlayerControllerDescriptionId);
            _playerController = new PlayerController(playerControllerDescription.AbilitiesController, playerControllerDescription.RaycastSettings);
            _inputListenerService.RegisterObject(_playerController);
        }

        public void UpdateLocal(float deltaTime)
        {
            _updateLocalService.UpdateLocal(deltaTime);
        }

        public void FixedUpdateLocal()
        {
            _fixUpdateLocalService.FixedUpdateLocal();
        }
        
        private void InitServices()
        {
            _updateLocalService = new UpdateLocalService();
            ServiceLocator.SetService(_updateLocalService);
            _fixUpdateLocalService = new FixUpdateLocalService();
            ServiceLocator.SetService(_fixUpdateLocalService);
            _spawnService = new SpawnService();
            ServiceLocator.SetService(_spawnService);
            _spawnService.Init();
            _inputListenerService =
                new InputListenerService(_library.GetInputDescription(_model.InputDescriptionId).Model);
            ServiceLocator.SetService(_inputListenerService);
        }

        private void OnLose()
        {
            _cameraController.SetActive(false);
            _gameUIController.OpenWindow(WindowIdentifiersMap.LoseWindow);
            _gameUIController.RestartButtonClicked += Restart;
        }

        private void OnWin()
        {
            _cameraController.SetActive(false);
            _playerModel.SetLevel(_playerModel.Level + 1);
            _gameUIController.SetLevel(_playerModel.Level);
            _gameUIController.SetMoney(_playerModel.Money);
            _gameUIController.OpenWindow(WindowIdentifiersMap.WinWindow);
            _gameUIController.NextButtonClicked += NextLevel;
        }

        private void NextLevel()
        {
            PrepareLevel();
            _gameUIController.NextButtonClicked -= NextLevel;
        }


        private void Restart()
        {
            PrepareLevel();
            _gameUIController.RestartButtonClicked -= Restart;
        }

        private void PrepareLevel()
        {
            _levelGenerator.GenerateLevel(_playerModel.Level);
            _cameraController.ResetCamera();
            _unitController.Reset();
            _unitController.HandleState(_unitController.StandUpState);
            _gameUIController.HideAllWindows();
        }

        private void OnLevelGenerationFinished()
        {
            
        }
        
        private UnitController CreateUnit()
        {
            var description = _library.GetUnitDescription(_model.UnitDescriptionId);
            var spawnPoint = _spawnService.GetObjectsByPredicate(
                x => x.Data.Id == SpawnPointIdentifierMap.HeroPoint).First();
            var controller =
                new UnitController(description.Model, description.Prefab, spawnPoint.Data);
            
            controller.Dead += OnLose;
            controller.CrossFinishLine += OnWin;
            return controller;
        }

        public void Dispose()
        {
        }
    }
}
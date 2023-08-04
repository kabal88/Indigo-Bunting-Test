using Data;
using Interfaces;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    public class CameraController : IInputListener, IUpdatable, IActivatable
    {
        private CameraView _view;
        private CameraModel _model;
        
        private Vector2 _startDraggingPosition;
        private Vector2 _currentPosition;
        private Vector3 _startUnitPos;
        
        private InputAction _positionUpdateAction;
        private bool _isActive;
        
        public bool IsAlive { get; }
        public Camera Camera => _view.Camera;

        public CameraController(CameraView view, CameraModel model)
        {
            _view = view;
            _model = model;
            IsAlive = true;
        }

        public void Init()
        {
            _view.Init();
            _model.SetStartUnitHolderLocalPosition(_view.UnitHolder.transform.localPosition);
            _model.SetUnitHolderLocalPosition(_view.UnitHolder.transform.localPosition);
            _model.SetStartPosition(_view.transform.position);
            var input = ServiceLocator.Get<InputListenerService>();
            input.TryGetInputAction(IdentifierToStringMap.Point, out _positionUpdateAction);
        }

        public void UpdateLocal(float deltaTime)
        {
            if (!_isActive)
                return;
            
            if (_model.CanMove)
            {
                MoveCamera(deltaTime);
            }
           
        }

        public void CommandReact(InputStartedCommand command)
        {
            if (!_isActive)
                return;
            
            if (command.Index == InputIdentifierMap.Fire)
            {
                if (!_model.CanMove)
                {
                    _model.SetCanMove(true);
                }

                if (_model.CanMove)
                {
                    _startDraggingPosition = _positionUpdateAction.ReadValue<Vector2>();
                    _startUnitPos = _model.UnitHolderLocalPosition;
                }
            }
        }

        public void CommandReact(InputCommand command)
        {
            if (!_isActive)
                return;
            
            if (command.Index != InputIdentifierMap.Fire || !_model.CanMove)
                return;

            _currentPosition = _positionUpdateAction.ReadValue<Vector2>();

            if (_startDraggingPosition == Vector2.zero)
                _startDraggingPosition = _currentPosition;

            var startToSpace = Camera.ScreenToWorldPoint(new Vector3(_startDraggingPosition.x, _startDraggingPosition.y,
                Camera.nearClipPlane));
            var currentToSpace = Camera.ScreenToWorldPoint(new Vector3(_currentPosition.x,
                _currentPosition.y, Camera.nearClipPlane));

            var dir = currentToSpace.x - startToSpace.x;
            var newPos = _model.UnitHolderLocalPosition;
            newPos.x = _startUnitPos.x + dir * _model.SideSpeed;
            newPos.x = Mathf.Clamp(newPos.x, _model.LeftBorder, _model.RightBorder);
            _view.UnitHolder.transform.localPosition = newPos;
            _model.SetUnitHolderLocalPosition(newPos);
        }

        public void CommandReact(InputEndedCommand command)
        {
        }

        private void MoveCamera(float deltaTime)
        {
            var currentSpeed = _model.ForwardSpeed * deltaTime;
            var transform = _view.transform;
            transform.Translate(Vector3.forward * currentSpeed);
        }
        
        public void ResetCamera()
        {
            _view.transform.position = _model.StartPosition;
            _view.UnitHolder.transform.localPosition = _model.StartUnitHolderLocalPosition;
            _model.SetCanMove(false);
            SetActive(true);
        }

        public void SetActive(bool isOn)
        {
            _isActive = isOn;
        }
    }
}
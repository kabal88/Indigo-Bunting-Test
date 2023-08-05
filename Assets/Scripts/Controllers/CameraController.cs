using System;
using Interfaces;
using UnityEngine;

namespace Controllers
{
    public class CameraController : IUpdatable, IActivatable, IDisposable
    {
        private CameraView _view;
        private CameraModel _model;

        private Vector2 _startDraggingPosition;
        private Vector2 _currentPosition;
        private Vector3 _startUnitPos;

        private bool _isActive;

        public bool IsAlive { get; }
        public Camera Camera => _view.Camera;

        public CameraController(CameraView view, CameraModel model)
        {
            _view = view;
            _model = model;
            IsAlive = true;
        }

        public void SetTarget(ICameraTarget target)
        {
            _model.SetHasTarget(target != null);
            _model.SetTarget(target);
        }

        public void Init()
        {
            _view.Init();
            _model.SetStartPosition(_view.transform.position);
        }

        public void UpdateLocal(float deltaTime)
        {
            if (!_isActive)
                return;

            if (_model.HasTarget)
            {
                MoveCamera();
            }
        }


        private void MoveCamera()
        {
            var transform = _view.transform;
            var position = Vector3.Lerp(transform.position, _model.Target.Position, _model.MovingRatio);
            transform.position = position;
        }

        public void ResetCamera()
        {
            // _view.transform.position = _model.StartPosition;
            // _model.SetHasTarget(false);
            // SetActive(true);
        }

        public void SetActive(bool isOn)
        {
            _isActive = isOn;
        }

        public void Dispose()
        {
            _model?.Dispose();
            _model = null;
            
            MonoBehaviour.Destroy(_view.gameObject);
        }
    }
}
using System;
using Components;
using DG.Tweening;
using Interfaces;
using Tweens;
using UnityEngine;

namespace Views
{
    public class UnitView : MonoBehaviour
    {
        [SerializeField] private ScaleParams _collectScaleParams;
        [SerializeField] private JumpParams _deadParams;


        private CollisionProvider _collisionProvider;
        private TrailHolder _trailHolder;
        private Tween _tween;

        public Vector3 Position => transform.position;

        public ICollisionProvider CollisionProvider => _collisionProvider;

        public void Init()
        {
            _collisionProvider = GetComponentInChildren<CollisionProvider>();
            _trailHolder = GetComponentInChildren<TrailHolder>();
        }

        public void SetNumber(int value)
        {
            PlayCollectAnimation();
        }

        private void PlayCollectAnimation()
        {
            // _numberMeshView.transform.localScale = Vector3.one * _collectScaleParams.StartScale;
            // _tween?.Kill();
            // _tween = _numberMeshView.transform.DOScale(_collectScaleParams.Target, _collectScaleParams.Duration)
            //     .SetEase(_collectScaleParams.Ease);
        }

        public void PlayFallingAnimation(Action onComplete = null)
        {
            // _tween?.Kill();
            // var target = transform.localPosition - _fallingMoveParams.Target;
            // _tween = transform.DOLocalMove(target, _fallingMoveParams.Duration)
            //     .SetEase(_fallingMoveParams.Ease)
            //     .OnComplete(() => { onComplete?.Invoke(); });
        }

        public void PlayDeadAnimation(Action onComplete = null)
        {
            _tween?.Kill();
            var target = transform.localPosition + _deadParams.EndValue;
            _tween = transform.DOLocalJump(target, _deadParams.JumpPower, _deadParams.NumJumps, _deadParams.Duration,
                    _deadParams.Snapping)
                .SetEase(_deadParams.Ease)
                .OnComplete(() => { onComplete?.Invoke(); });
        }

        public void SetTrailActive(bool value)
        {
            _trailHolder.SetTrailActive(value);
        }

        private void OnDestroy()
        {
            _collisionProvider = null;
            _tween?.Kill();
        }
    }
}
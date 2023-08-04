using System;
using Components;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Views
{
    public class UnitView : MonoBehaviour
    {
        public event Action<int> AnimationEvent
        {
            add => _animationEventProvider.AnimationEvent += value;
            remove => _animationEventProvider.AnimationEvent -= value;
        }

        [SerializeField] private GameObject _positionHolder;
        [SerializeField] private Rigidbody[] _rigidbodies;
        [SerializeField] private Collider[] _colliders;
        [SerializeField] private Animator _animator;

        private CollisionProvider _collisionProvider;
        private AnimationEventProvider _animationEventProvider;

        public Vector3 Position => transform.position;

        public Vector3 PositionForCamera => _positionHolder.transform.position;

        public ICollisionProvider CollisionProvider => _collisionProvider;

        public void Init()
        {
            _collisionProvider = GetComponentInChildren<CollisionProvider>();
            _animationEventProvider = GetComponentInChildren<AnimationEventProvider>();
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
            // _tween?.Kill();
            // var target = transform.localPosition + _deadParams.EndValue;
            // _tween = transform.DOLocalJump(target, _deadParams.JumpPower, _deadParams.NumJumps, _deadParams.Duration,
            //         _deadParams.Snapping)
            //     .SetEase(_deadParams.Ease)
            //     .OnComplete(() => { onComplete?.Invoke(); });
        }

        public void SetRigidbodiesKinematic(bool value)
        {
            foreach (var r in _rigidbodies)
            {
                r.isKinematic = value;
            }
        }

        public void SetAnimatorEnabled(bool value)
        {
            _animator.enabled = value;
        }

        private void OnDestroy()
        {
            _collisionProvider = null;
            _positionHolder = null;
            _rigidbodies = null;
        }

        [Button]
        private void CollectRigidbodies()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
        }

        [Button]
        private void CollectColliders()
        {
            _colliders = GetComponentsInChildren<Collider>();
        }
    }
}
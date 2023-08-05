using System;
using Components;
using Interfaces;
using Models;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Views
{
    public class UnitView : MonoBehaviour, ITarget, IHaveUnitContext
    {
        public event Action<int> AnimationEvent
        {
            add => _animationEventProvider.AnimationEvent += value;
            remove => _animationEventProvider.AnimationEvent -= value;
        }

        [SerializeField] private GameObject _positionHolder;
        [SerializeField] private Rigidbody[] _rigidbodies;
        [SerializeField] private Animator _animator;

        private CollisionProvider _collisionProvider;
        private AnimationEventProvider _animationEventProvider;
        private static readonly int State = Animator.StringToHash("State");
        private static readonly int InAction = Animator.StringToHash("InAction");

        public bool IsAlive => UnitContext.Model.IsAlive;
        
        public Vector3 Position => transform.position;

        public Vector3 PositionForCamera => _positionHolder.transform.position;

        public ICollisionProvider CollisionProvider => _collisionProvider;

        public IUnitContext UnitContext { get; private set; }

        public void Init(IUnitContext unitContext)
        {
            UnitContext = unitContext;
            _collisionProvider = GetComponentInChildren<CollisionProvider>();
            _animationEventProvider = GetComponentInChildren<AnimationEventProvider>();
            AnimationEvent += OnInActionAnimationEvent;
        }


        public void SetAnimationState(int stateId)
        {
            _animator.SetBool(InAction, false);
            _animator.SetInteger(State, stateId);
        }
        
        public void OnInActionAnimationEvent(int eventId)
        {
            if (eventId == AnimationEventIdentifierMap.InAction)
            {
                _animator.SetBool(InAction, true);
            }
            
        }

        public void PlayDeadAnimation(Action onComplete = null)
        {

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

        public Rigidbody GetHittedRigidbody(Vector3 hitPoint)
        {
            float closestDistance = 0;
            var index = 0;

            for (var i = 0; i < _rigidbodies.Length; i++)
            {
                if (i == 0)
                {
                    closestDistance = SqrDistance(_rigidbodies[i].position, hitPoint);
                    continue;
                }
                
                float distance = SqrDistance(_rigidbodies[i].position, hitPoint);

                if ( distance < closestDistance)
                {
                    closestDistance = distance;
                    index = i;
                }
            }

            return _rigidbodies[index];
        }

        private float SqrDistance(Vector3 startPoint, Vector3 endPoint)
        {
            var dir = endPoint - startPoint;
            return Vector3.SqrMagnitude(dir);
        }

        private void OnDestroy()
        {
            _collisionProvider = null;
            _positionHolder = null;
            _rigidbodies = null;
            AnimationEvent -= OnInActionAnimationEvent;
        }

        [Button]
        private void CollectRigidbodies()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
        }
    }
}
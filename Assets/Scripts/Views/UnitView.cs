using System;
using System.Collections.Generic;
using Components;
using Cysharp.Threading.Tasks;
using Data;
using Helpers;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Views
{
    public class UnitView : MonoBehaviour, ITargetWithBones, IHaveUnitContext, ICollisionProvider, IMoneyCollector
    {
        public event Action<int> AnimationEvent
        {
            add => _animationEventProvider.AnimationEvent += value;
            remove => _animationEventProvider.AnimationEvent -= value;
        }

        public event Action<Collision> CollisionEnter;
        public event Action<Collision> CollisionExit;
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;

        public event Action<int> CollectMoney;

        [SerializeField] private GameObject _positionHolder;

        [SerializeField, BoxGroup("Animation")]
        private Animator _animator;

        [SerializeField] private LayerMask _floorLayerMask;
        [SerializeField, BoxGroup("Bones")] private Transform _hipBone;
        [SerializeField, BoxGroup("Bones")] private Rigidbody _hipBoneRigidbody;

        [FormerlySerializedAs("_bones")] [SerializeField, BoxGroup("Bones")]
        private Transform[] _bonesTransforms;

        [SerializeField, BoxGroup("Rigidbody")]
        private Rigidbody[] _rigidbodies;

        [SerializeField, BoxGroup("Animation")]
        private BoneData[] _standUpAnimationStartBoneData;

        private RaycastHit _hitInfo;
        private AnimationEventProvider _animationEventProvider;
        private List<ICollisionProvider> _collisionProviders = new();
        private BoneTag[] _bones;

        private static readonly int State = Animator.StringToHash("State");
        private static readonly int InAction = Animator.StringToHash("InAction");

        public bool IsAlive => UnitContext.Model.IsAlive;

        public Vector3 Position => transform.position;

        public Vector3 PositionForCamera => _positionHolder.transform.position;

        public float SqrSpeedOfHip => _hipBoneRigidbody.velocity.sqrMagnitude;

        public IUnitContext UnitContext { get; private set; }

        public Transform[] BonesTransforms => _bonesTransforms;
        public IEnumerable<BoneTag> Bones => _bones;

        public int BonesCount => _bonesTransforms.Length;

        public BoneData[] StandUpAnimationStartBoneData => _standUpAnimationStartBoneData;

        public void Init(IUnitContext unitContext)
        {
            UnitContext = unitContext;
            _animationEventProvider = GetComponentInChildren<AnimationEventProvider>();
            AnimationEvent += OnInActionAnimationEvent;

            _bones = GetComponentsInChildren<BoneTag>();

            foreach (var b in _bones)
            {
                b.SetOwner(this);
                var provider = b.gameObject.GetOrAddComponent<CollisionProvider>();
                provider.CollisionEnter += OnCollisionEnter;
                provider.CollisionExit += OnCollisionExit;
                provider.TriggerEnter += OnTriggerEnter;
                provider.TriggerExit += OnTriggerExit;
                _collisionProviders.Add(provider);
            }
        }

        public void SetAnimationState(int stateId)
        {
            _animator.SetInteger(State, stateId);
            _animator.SetBool(InAction, false);
            Debug.Log($"{stateId} false");
            SetInAction();
        }

        private void OnInActionAnimationEvent(int eventId)
        {
            // if (eventId == AnimationEventIdentifierMap.InAction)
            // {
            //     _animator.SetBool(InAction, true);
            // }
        }

        private async void SetInAction()
        {
            await UniTask.NextFrame();
            _animator.SetBool(InAction, true);
            Debug.Log($"InAction true");
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

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    index = i;
                }
            }

            return _rigidbodies[index];
        }

        public void AddMoney(int value)
        {
            CollectMoney?.Invoke(value);
        }

        private void OnCollisionEnter(Collision obj)
        {
            CollisionEnter?.Invoke(obj);
        }

        private void OnCollisionExit(Collision obj)
        {
            CollisionExit?.Invoke(obj);
        }

        private void OnTriggerEnter(Collider obj)
        {
            TriggerEnter?.Invoke(obj);
        }

        private void OnTriggerExit(Collider obj)
        {
            TriggerExit?.Invoke(obj);
        }

        public void AlignParentTransformToHips()
        {
            var originalHipsPosition = _hipBone.position;
            transform.position = originalHipsPosition;

            if (Physics.Raycast(transform.position, Vector3.down, out _hitInfo, 10f, _floorLayerMask))
            {
                transform.position = new Vector3(transform.position.x, _hitInfo.point.y, transform.position.z);
            }


            _hipBone.position = originalHipsPosition;
        }

        private float SqrDistance(Vector3 startPoint, Vector3 endPoint)
        {
            var dir = endPoint - startPoint;
            return Vector3.SqrMagnitude(dir);
        }

        private void OnDestroy()
        {
            _positionHolder = null;
            _rigidbodies = null;
            AnimationEvent -= OnInActionAnimationEvent;
            _animationEventProvider = null;
            _animator = null;
            _hipBone = null;
            _hipBoneRigidbody = null;
            _bonesTransforms = null;
            _standUpAnimationStartBoneData = null;
            
            foreach (var c in _collisionProviders)
            {
                c.CollisionEnter -= OnCollisionEnter;
                c.CollisionExit -= OnCollisionExit;
                c.TriggerEnter -= OnTriggerEnter;
                c.TriggerExit -= OnTriggerExit;
            }

            _collisionProviders = null;
        }

        #region Editor

#if UNITY_EDITOR

        [Button, BoxGroup("Rigidbody")]
        private void CollectRigidbodies()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
        }

        [Button, BoxGroup("Test")]
        private void StandUp()
        {
            UnitContext.HandleState(UnitContext.StandUpState);
        }

        [Button, BoxGroup("Animation")]
        private void SnapshotStartBoneData()
        {
            var bones = _hipBone.GetComponentsInChildren<Transform>();
            _standUpAnimationStartBoneData = new BoneData[bones.Length];
            _standUpAnimationStartBoneData.SnapshotBoneData(bones);
        }

        [Button("Snapshot from anim clip"), BoxGroup("Animation")]
        private void SnapshotStartBoneDataFromAnimationClip(string clipName = "Stand Up")
        {
            var bones = _hipBone.GetComponentsInChildren<Transform>();
            _standUpAnimationStartBoneData = new BoneData[bones.Length];

            foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == clipName)
                {
                    clip.SampleAnimation(gameObject, 0);
                    _standUpAnimationStartBoneData.SnapshotBoneData(bones);
                    Debug.Log("Snapshot done");
                    return;
                }
            }

            Debug.Log("No clip found");
        }

        [Button, BoxGroup("Bones")]
        private void CollectBones()
        {
            _bonesTransforms = _hipBone.GetComponentsInChildren<Transform>();
        }
#endif

        #endregion
    }
}
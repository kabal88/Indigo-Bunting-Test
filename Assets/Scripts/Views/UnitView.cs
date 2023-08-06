using System;
using Components;
using Data;
using Helpers;
using Interfaces;
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

        public event Action<int> CollectMoney;

        [SerializeField] private GameObject _positionHolder;
        [SerializeField] private CollisionProvider _playerCollisionProvider;
        [SerializeField] private CollisionProvider _floorCollisionProvider;
        [SerializeField, BoxGroup("Animation")]  private Animator _animator;
        [SerializeField] private LayerMask _floorLayerMask;
        [SerializeField, BoxGroup("Bones")] private Transform _hipBone;
        [SerializeField, BoxGroup("Bones")] private Rigidbody _hipBoneRigidbody;
        [SerializeField, BoxGroup("Bones")] private Transform[] _bones;
        [SerializeField, BoxGroup("Rigidbody")] private Rigidbody[] _rigidbodies;
        [SerializeField, BoxGroup("Animation")] private BoneData[] _standUpAnimationStartBoneData;

        private RaycastHit _hitInfo;
        private AnimationEventProvider _animationEventProvider;
        private static readonly int State = Animator.StringToHash("State");
        private static readonly int InAction = Animator.StringToHash("InAction");

        public bool IsAlive => UnitContext.Model.IsAlive;

        public Vector3 Position => transform.position;

        public Vector3 PositionForCamera => _positionHolder.transform.position;

        public ICollisionProvider PlayerCollisionProvider => _playerCollisionProvider;
        public ICollisionProvider FloorCollisionProvider => _floorCollisionProvider;

        public float SqrSpeedOfHip => _hipBoneRigidbody.velocity.sqrMagnitude;

        public IUnitContext UnitContext { get; private set; }

        public Transform[] Bones => _bones;

        public int BonesCount => _bones.Length;

        public BoneData[] StandUpAnimationStartBoneData => _standUpAnimationStartBoneData;

        public void Init(IUnitContext unitContext)
        {
            UnitContext = unitContext;
            _animationEventProvider = GetComponentInChildren<AnimationEventProvider>();
            AnimationEvent += OnInActionAnimationEvent;
            _floorCollisionProvider.TriggerEnter += OnFloorCollisionEnter;
            
            var bones = GetComponentsInChildren<BoneTag>();
            foreach (var b in bones)
            {
                b.SetOwner(this);
                //todo нужно добавить отслеживание коллизий с объектами
            }
        }

        private void OnFloorCollisionEnter(Collider obj)
        {
            Debug.Log($"Floor collision enter {obj.name}");
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
            _playerCollisionProvider = null;
            _floorCollisionProvider = null;
            _positionHolder = null;
            _rigidbodies = null;
            AnimationEvent -= OnInActionAnimationEvent;
        }
        
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
            _bones = _hipBone.GetComponentsInChildren<Transform>();
        }
#endif
    }
}
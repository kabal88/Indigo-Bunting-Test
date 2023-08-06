using System;
using Data;
using Helpers;
using Interfaces;
using UnityEngine;


namespace Controllers.UnitStates
{
    public class StandUpState : UnitStateBase
    {
        private BoneData[] _ragdollBoneData;
        private bool _isTransitioningToAnimationState;
        private float _timeToResetBones = 1f;
        private float _elapsedResetBonesTime;

        public StandUpState(IUnitContext unit) : base(unit)
        {
            _ragdollBoneData = new BoneData[Unit.View.BonesCount];
        }

        public override void HandleState(UnitStateBase newState)
        {
            switch (newState)
            {
                case IdleState:
                    Unit.SetState(Unit.IdleState);
                    break;
            }
        }

        public override void StartState()
        {
            Debug.Log("StandUpState: StartState");
            _elapsedResetBonesTime = 0;
            Unit.View.AlignParentTransformToHips();
            _ragdollBoneData.SnapshotBoneData(Unit.View.BonesTransforms);
            _isTransitioningToAnimationState = true;
            
        }

        private void OnAnimationEvent(int id)
        {
            if (id == AnimationEventIdentifierMap.StandUpFinished)
            {
                HandleState(Unit.IdleState);
            }
        }

        public override void UpdateLocal(float deltaTime)
        {
            if (_isTransitioningToAnimationState)
            {
                TransitingBonesToAnimationState(OnTransitionFinished);
            }
            
        }

        private void TransitingBonesToAnimationState(Action callback)
        {
            _elapsedResetBonesTime += Time.deltaTime;
            float elapsedPercentage = _elapsedResetBonesTime / _timeToResetBones;
            var bones = Unit.View.BonesTransforms;
            var animStartState = Unit.View.StandUpAnimationStartBoneData;

            for (int i = 0; i < bones.Length; i ++)
            {
                bones[i].localPosition = Vector3.Lerp(
                    _ragdollBoneData[i].Position,
                    animStartState[i].Position,
                    elapsedPercentage);

                bones[i].localRotation = Quaternion.Lerp(
                    _ragdollBoneData[i].Rotation,
                    animStartState[i].Rotation,
                    elapsedPercentage);
            }

            if (elapsedPercentage >=1)
            {
                _isTransitioningToAnimationState = false;
                _elapsedResetBonesTime = 0;
                callback?.Invoke();
            }
        }

        private void OnTransitionFinished()
        {
            Unit.View.SetRigidbodiesKinematic(true);
            Unit.View.SetAnimatorEnabled(true);
            Unit.View.AnimationEvent += OnAnimationEvent;
            Unit.View.SetAnimationState(AnimationStateIdentifierMap.StandUp);
        }

        public override void EndState()
        {
            Unit.View.AnimationEvent -= OnAnimationEvent;
            Debug.Log("StandUpState: EndState");
        }
    }
}
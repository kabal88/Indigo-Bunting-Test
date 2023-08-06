using System;
using Interfaces;
using UnityEngine;

[Serializable]
public class PushBackAbility : IAbility, IDisposable
{
    [SerializeField] private float _throwPower = 50f;
    
    private BoneTag _closestBone;

    public void Execute(IOwner owner = null, ITarget target = null)
    {
        if (target is ITargetWithBones targetWithBones)
        {
            if (owner == null)
                return;

            
            var closestDistance = float.MaxValue;
            var hasBone = false;

            foreach (var b in targetWithBones.Bones)
            {
                var distance = Vector3.SqrMagnitude(owner.Transform.position - b.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _closestBone = b;
                    hasBone = true;
                }
            }

            if (hasBone)
            {
                var direction = (_closestBone.transform.position - owner.Transform.position).normalized;
                _closestBone.Rigidbody.AddForce(direction * _throwPower, ForceMode.Impulse);
            }
            
        }
    }

    public void Dispose()
    {
        if (_closestBone != null)
        {
            _closestBone = null;
        }
    }
}
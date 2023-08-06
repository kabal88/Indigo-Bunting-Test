using System;
using Interfaces;
using UnityEngine;

[Serializable]
public class PushBackAbility : IAbility
{
    [SerializeField] private float _throwPower = 50f;

    public void Execute(IOwner owner = null, ITarget target = null)
    {
        if (target is BoneTag bone)
        {
            if (bone.TryGetComponent(out Rigidbody rigidbody))
            {
                if (owner != null)
                {
                    var direction = (bone.transform.position - owner.Transform.position).normalized;
                    rigidbody.AddForce(direction * _throwPower, ForceMode.Impulse);
                }
                else
                {
                    rigidbody.AddForce(Vector3.up * _throwPower, ForceMode.Impulse);
                }
            }
        }
    }
}
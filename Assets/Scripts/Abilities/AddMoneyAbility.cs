using System;
using Interfaces;
using UnityEngine;

[Serializable]
public class AddMoneyAbility : IAbility
{
    [SerializeField] private int _value = 1;
    public void Execute(IOwner owner = null, ITarget target = null)
    {
        if (target is BoneTag bone)
        {
            bone.Owner.AddMoney(_value);
        }
    }
}

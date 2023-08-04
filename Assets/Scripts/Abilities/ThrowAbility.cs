using System;
using Interfaces;
using Tweens;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public class ThrowAbility : IAbility
    {
        [SerializeField] private JumpParams _throwParams;

        public void Execute(IOwner owner = null, ITarget target = null)
        {
            // todo add power to throwing point
        }
    }
}
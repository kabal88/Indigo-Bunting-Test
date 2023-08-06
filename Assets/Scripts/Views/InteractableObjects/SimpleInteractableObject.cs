using System;
using Abilities;
using Interfaces;
using UnityEngine;

namespace Views
{
    public class SimpleInteractableObject : InteractableObject
    {
        [SerializeField] protected AbilityDescription Ability;

        private IDisposable _disposable;

        public override void Interact(IOwner owner = null, ITarget target = null)
        {
            Ability.GetAbility.Execute(owner, target);
        }

        private void Awake()
        {
            var ability = Ability.GetAbility;
            if (ability is IInitable initable)
            {
                initable.Init();
            }

            if (ability is IDisposable disposable)
            {
                _disposable = disposable;
            }
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}
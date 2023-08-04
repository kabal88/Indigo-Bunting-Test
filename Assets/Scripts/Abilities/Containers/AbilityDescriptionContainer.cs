using Interfaces;
using UnityEngine;

namespace Abilities
{
    public abstract class AbilityDescriptionContainer<T> : AbilityDescription where T : IAbility, new()
    {
        [SerializeField] private T _ability = new T();
        
        public override IAbility GetAbility => _ability;
    }
}
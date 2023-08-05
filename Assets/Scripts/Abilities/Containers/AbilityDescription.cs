using Identifier;
using Interfaces;
using UnityEngine;

namespace Abilities
{
    public abstract class AbilityDescription : ScriptableObject, IIdentifier
    {
        [SerializeField] private AbilitiesIdentifier _identifier;
        public int Id => _identifier.Id;
        public abstract IAbility GetAbility { get;}
    }
}
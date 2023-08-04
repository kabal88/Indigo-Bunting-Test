using Interfaces;
using UnityEngine;

namespace Abilities
{
    public abstract class AbilityDescription : ScriptableObject
    {
        public abstract IAbility GetAbility { get;}
    }
}
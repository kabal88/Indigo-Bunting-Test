using System;
using Abilities;
using Controllers;
using Data;
using Identifier;
using Interfaces;
using UnityEngine;

namespace Descriptions
{
    [Serializable]
    public class PlayerControllerDescription : IPlayerControllerDescription
    {
        [SerializeField] private PlayerControllerIdentifier _id;
        [SerializeField] private AbilityDescription[] _abilities;
        [SerializeField] private RaycastSettings _raycastSettings;

        public int Id => _id.Id;

        public AbilitiesController AbilitiesController => new(_abilities);
        public RaycastSettings RaycastSettings => _raycastSettings;
    }
}
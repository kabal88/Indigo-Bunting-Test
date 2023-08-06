using System;
using Data;
using Interfaces;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    public class PlayerController : IInputListener, IDisposable, IOwner
    {
        private InputAction _positionUpdateAction;
        private AbilitiesController _abilitiesController;
        private RaycastHit[] _hits;
        private float _distance;
        private LayerMask _layerMask;

        public bool IsAlive { get; }

        public Transform Transform { get; }

        public PlayerController(AbilitiesController abilitiesController, RaycastSettings raycastSettings)
        {
            _abilitiesController = abilitiesController;
            var input = ServiceLocator.Get<InputListenerService>();
            input.TryGetInputAction(IdentifierToStringMap.Point, out _positionUpdateAction);
            _hits = new RaycastHit[1];
            _distance = raycastSettings.Distance;
            _layerMask = raycastSettings.LayerMask;
        }

        public void CommandReact(InputStartedCommand command)
        {
            if (command.Index == InputIdentifierMap.Fire)
            {
                var pos = _positionUpdateAction.ReadValue<Vector2>();
                var ray = Camera.main.ScreenPointToRay(pos);
                Physics.RaycastNonAlloc(ray, _hits, _distance, _layerMask);


                var hit = _hits[0];
                if (hit.collider == null)
                    return;


                if (hit.collider.gameObject.TryGetComponent<ITarget>(out var target))
                {
                    _abilitiesController.ExecuteAbility(AbilitiesIdentifierMap.ThrowAbilityID, target: target);
                }
            }
        }

        public void CommandReact(InputCommand command)
        {
        }

        public void CommandReact(InputEndedCommand command)
        {
        }

        public void Dispose()
        {
            _positionUpdateAction = null;
            _abilitiesController?.Dispose();
        }
    }
}
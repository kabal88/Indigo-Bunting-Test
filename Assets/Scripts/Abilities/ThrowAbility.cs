using System;
using Interfaces;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Abilities
{
    [Serializable]
    public class ThrowAbility : IAbility, IInitable, IDisposable
    {
        [SerializeField] private float _throwPower = 50f;
        [SerializeField] private float _distance = 500f;
        [SerializeField] private LayerMask _layerMask;

        private InputAction _positionUpdateAction;
        private RaycastHit[] _hits = new RaycastHit[8];

        public void Init()
        {
            var input = ServiceLocator.Get<InputListenerService>();
            input.TryGetInputAction(IdentifierToStringMap.Point, out _positionUpdateAction);
        }

        public void Execute(IOwner owner = null, ITarget target = null)
        {
            if (target is IHaveUnitContext context)
            {
                var unit = context.UnitContext;
                if (unit == null)
                    return;

                if (!unit.Model.IsInteractable)
                    return;

                unit.HandleState(unit.FallingState);

                var pos = _positionUpdateAction.ReadValue<Vector2>();
                var ray = Camera.main.ScreenPointToRay(pos);
                Physics.RaycastNonAlloc(ray, _hits, _distance, _layerMask);

                var hit = _hits[0];
                if (hit.collider == null)
                    return;

                var hitPoint = hit.point;
                var hitRigidbody = unit.View.GetHittedRigidbody(hitPoint);
                hitRigidbody.AddForceAtPosition(ray.direction * _throwPower, hitPoint, ForceMode.Impulse);
            }
        }

        public void Dispose()
        {
            _positionUpdateAction = null;
        }
    }
}
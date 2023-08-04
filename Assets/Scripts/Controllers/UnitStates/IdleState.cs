using Data;
using Interfaces;
using Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers.UnitStates
{
    public class IdleState : UnitStateBase, IInputListener
    {
        private InputAction _positionUpdateAction;
        private RaycastHit[] _hits = new RaycastHit[8];
        private float _distance = 500;
        private LayerMask _layerMask;

        public IdleState(IUnitContext unit) : base(unit)
        {
            var input = ServiceLocator.Get<InputListenerService>();
            input.TryGetInputAction(IdentifierToStringMap.Point, out _positionUpdateAction);
            _layerMask = LayerMask.NameToLayer("Interactive");
        }

        public override void HandleState(UnitStateBase newState)
        {
            switch (newState)
            {
                case FallingState state:
                    Unit.SetState(state);
                    break;
            }
        }

        public override void StartState()
        {
            ServiceLocator.Get<InputListenerService>().RegisterObject(this);
        }

        public override void UpdateLocal(float deltaTime)
        {
        }

        public override void EndState()
        {
            ServiceLocator.Get<InputListenerService>().UnRegisterObject(this);
            for (int i = 0; i < _hits.Length; i++)
            {
                _hits[i] = default;
            }
        }

        public void CommandReact(InputStartedCommand command)
        {
            if (command.Index == InputIdentifierMap.Fire)
            {
                //HandleState(Unit.FallingState);

                var pos = _positionUpdateAction.ReadValue<Vector2>();
                var ray = Camera.main.ScreenPointToRay(pos);
                Physics.RaycastNonAlloc(ray, _hits, _distance, _layerMask);

                if (_hits.Length > 0)
                {
                    if (_hits[0].collider.gameObject.TryGetComponent<ITarget>(out var target))
                    {
                        
                    }
                }
            }
            // todo add power hero and switch to ragdoll
        }

        public void CommandReact(InputCommand command)
        {
        }

        public void CommandReact(InputEndedCommand command)
        {
        }

        public override void Dispose()
        {
            base.Dispose();
            _positionUpdateAction = null;
            _hits = null;
        }
    }
}
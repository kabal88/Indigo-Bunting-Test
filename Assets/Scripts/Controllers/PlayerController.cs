using Data;
using Interfaces;
using Services;
using UnityEngine.InputSystem;

namespace Controllers
{
    public class PlayerController : IInputListener, IActivatable
    {
        private bool _isActive;
        private InputAction _positionUpdateAction;
        
        public bool IsAlive { get; }

        public void Init()
        {
            var input = ServiceLocator.Get<InputListenerService>();
            input.TryGetInputAction(IdentifierToStringMap.Point, out _positionUpdateAction);
        }
        
        public void CommandReact(InputStartedCommand command)
        {
            throw new System.NotImplementedException();
        }

        public void CommandReact(InputCommand command)
        {
            throw new System.NotImplementedException();
        }

        public void CommandReact(InputEndedCommand command)
        {
            throw new System.NotImplementedException();
        }

        public void SetActive(bool isOn)
        {
            _isActive = isOn;
        }
    }
}
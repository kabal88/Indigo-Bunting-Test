using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Helpers;
using Interfaces;
using Models;
using UnityEngine.InputSystem;

namespace Services
{
    public class InputListenerService : IRepository<IInputListener>, IUpdatable
    {
        private InputActionsModel _model;
        
        private readonly List<IInputListener> _listeners;
        
        private Queue<IInputListener> _addListeners;
        private Queue<IInputListener> _removeListeners;
        
        public bool IsAlive { get; }

        public InputListenerService(InputActionsModel model)
        {
            _model = model;
            LinkActions();
            _listeners = new List<IInputListener>();
            _addListeners = new Queue<IInputListener>();
            _removeListeners = new Queue<IInputListener>();
            IsAlive = true;
            
            ServiceLocator.Get<UpdateLocalService>().RegisterObject(this);
        }
        
        public void UpdateLocal(float deltaTime)
        {
            for (var i = 0; i < _addListeners.Count; i++)
            {
                _listeners.Add(_addListeners.Dequeue());
            }

            for (var i = 0; i < _removeListeners.Count; i++)
            {
                _listeners.Remove(_removeListeners.Dequeue());
            }
            
            foreach (var action in _model.UpdateableActions)
                action.UpdateAction();
        }

        public void RegisterObject(IInputListener obj)
        {
            _addListeners.Enqueue(obj);
        }

        public void UnRegisterObject(IInputListener obj)
        {
            _removeListeners.Enqueue(obj);
        }

        public IEnumerable<IInputListener> GetObjectsByPredicate(Func<IInputListener, bool> predicate)
        {
            return _listeners.Where(predicate);
        }
        
        public bool TryGetInputAction(string name, out InputAction inputAction)
        {
            return _model.TryGetInputAction(name, out inputAction);
        }

        private void LinkActions()
        {
            
            var defaultActionMap = _model.Actions.actionMaps[0];
            defaultActionMap.Enable();

            foreach (var action in defaultActionMap.actions)
            {
                var neededIndex = _model.InputActionSettings.FirstOrDefault(x => x.ActionName == action.name);
                action.Enable();
                var updateableAction = new UpdateableAction(neededIndex.Identifier.Id, action);
                updateableAction.OnStart += OnActionStart;
                updateableAction.OnEnd += OnActionEnd;
                updateableAction.OnUpdate += OnActionUpdate;
                _model.UpdateableActions.Add(updateableAction);
            }
        }

        private void OnActionStart(int index, InputAction.CallbackContext context)
        {
            var command = new InputStartedCommand { Index = index, Context = context };

            foreach (var l in _listeners)
            {
                l.CommandReact(command);
            }
        }

        private void OnActionUpdate(int index, InputAction.CallbackContext context)
        {
            var command = new InputCommand { Index = index, Context = context };

            foreach (var l in _listeners)
            {
                l.CommandReact(command);
            }
        }

        private void OnActionEnd(int index, InputAction.CallbackContext context)
        {
            var command = new InputEndedCommand { Index = index, Context = context };

            foreach (var l in _listeners)
            {
                l.CommandReact(command);
            }
        }
    }
}
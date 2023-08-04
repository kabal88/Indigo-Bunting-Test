using System;
using Controllers.UnitStates;
using Data;
using Interfaces;
using Models;
using Services;
using UnityEngine;
using Views;

namespace Controllers
{
    public class UnitController : IUpdatable, IActivatable, IUnitContext, IDisposable, ITarget
    {
        public event Action Dead;
        public event Action CrossFinishLine;
        public event Action<int> NumberChanged; 

        private UnitStateBase _currentState;

        public bool IsAlive => Model.IsAlive;
        public int CurrentNumber => Model.CurrentNumber;
        public IdleState IdleState { get; private set; }
        public DeadState DeadState { get; private set; }
        public MovingState MovingState { get; private set; }
        public CrossFinishLineState CrossFinishLineState { get; private set; }
        public JumpState JumpState { get; private set;}
        public FallingState FallingState { get; private set;}
        public UnitModel Model { get; private set; }
        public UnitView View { get; private set; }
        
        public ITarget Target => this;



        public UnitController(UnitModel model, GameObject prefab ,  Transform parant, SpawnData spawnData)
        {
            Model = model;
            
            View = GameObject.Instantiate(prefab, spawnData.Position, spawnData.Rotation).GetComponent<UnitView>();
            View.transform.SetParent(parant);
            View.Init();
            
            Model.SetStartLocalPosition(View.transform.localPosition);

            IdleState = new IdleState(this);
            DeadState = new DeadState(this);
            MovingState = new MovingState(this);
            CrossFinishLineState = new CrossFinishLineState(this);
            JumpState = new JumpState(this);
            FallingState = new FallingState(this);
            
            SetState(IdleState);

            ServiceLocator.Get<UpdateLocalService>().RegisterObject(this);
        }
        
        public void SetNumber(int value)
        {
            Model.SetCurrentNumber(value);
            View.SetNumber(Model.CurrentNumber);
            NumberChanged?.Invoke(Model.CurrentNumber);
        }
        
        public void SetActive(bool isOn)
        {
            Model.SetIsActive(isOn);
        }

        public void UpdateLocal(float deltaTime)
        {
            if (Model.IsActive)
            {
                _currentState.UpdateLocal(deltaTime);
            }
        }

        public void SetState(UnitStateBase newState)
        {
            _currentState?.EndState();
            _currentState = newState;
            _currentState.StartState();
        }

        public void HandleState(UnitStateBase newState)
        {
            _currentState.HandleState(newState);
        }

        public void OnDead()
        {
            Dead?.Invoke();
        }
        
        public void OnCrossFinishLine()
        {
            CrossFinishLine?.Invoke();
        }

        public void AddToCurrentNumber(int value)
        {
            Model.AddToCurrentNumber(value);
            View.SetNumber(Model.CurrentNumber);
            NumberChanged?.Invoke(Model.CurrentNumber);
        }

        public void Die()
        {
            HandleState(DeadState);
        }

        public void Reset()
        {
            SetState(IdleState);
            Model.SetIsActive(true);
            Model.SetIsAlive(true);
            var transform = View.transform;
            transform.localPosition = Model.StartLocalPosition;
        }

        public void Dispose()
        {
            ServiceLocator.Get<UpdateLocalService>().UnRegisterObject(this);

            IdleState.Dispose();
            IdleState = null;

            DeadState.Dispose();
            DeadState = null;
            
            MovingState.Dispose();
            MovingState = null;
            
            CrossFinishLineState.Dispose();
            CrossFinishLineState = null;
            
            JumpState.Dispose();
            JumpState = null;
            
            FallingState.Dispose();
            FallingState = null;

            _currentState.Dispose();
            _currentState = null;

            Model = null;

            GameObject.Destroy(View.gameObject);
            View = null;
        }
    }
}
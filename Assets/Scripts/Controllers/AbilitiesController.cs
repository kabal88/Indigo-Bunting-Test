using System;
using System.Collections.Generic;
using Abilities;
using Interfaces;

namespace Controllers
{
    public class AbilitiesController : IDisposable
    {
        private Dictionary<int, IAbility> _abilities;
        private List<IDisposable> _disposables;

        public AbilitiesController(AbilityDescription[] descriptions)
        {
            _abilities = new Dictionary<int, IAbility>();
            _disposables = new List<IDisposable>();
            foreach (var d in descriptions)
            {
                var ability = d.GetAbility;
                _abilities.Add(d.Id, ability);
                
                if (ability is IInitable initable)
                {
                    initable.Init();
                }

                if (ability is IDisposable disposable)
                {
                    _disposables.Add(disposable);
                }

            }

            foreach (var ability in _abilities)
            {
                if (ability.Value is IInitable initable)
                {
                    initable.Init();
                }
            }
        }

        public void ExecuteAbility(int id, IOwner owner = null, ITarget target = null)
        {
            if (_abilities.TryGetValue(id, out var ability))
            {
                ability.Execute(owner, target);
            }
        }

        public void Dispose()
        {
            _abilities.Clear();
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
        }
    }
}
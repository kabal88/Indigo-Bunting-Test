using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace Services
{
    public sealed class UpdateLocalService : IUpdateService
    {
        private readonly List<IUpdatable> _updatables;

        private Queue<IUpdatable> _addUpdatables;
        private Queue<IUpdatable> _removeUpdatables;

        public bool IsAlive { get; private set; }

        public UpdateLocalService()
        {
            _updatables = new List<IUpdatable>();
            _addUpdatables = new Queue<IUpdatable>();
            _removeUpdatables = new Queue<IUpdatable>();
            IsAlive = true;
        }

        public void RegisterObject(IUpdatable updatable)
        {
            _addUpdatables.Enqueue(updatable);
        }

        public void UnRegisterObject(IUpdatable updatable)
        {
            _removeUpdatables.Enqueue(updatable);
        }

        public IEnumerable<IUpdatable> GetObjectsByPredicate(Func<IUpdatable, bool> predicate)
        {
            return _updatables.Where(predicate);
        }

        public bool TryGetObject(out IUpdatable obj)
        {
            obj = _updatables.FirstOrDefault();
            return obj != null;
        }
        
        public void UpdateLocal(float deltaTime)
        {
            for (int i = 0; i < _addUpdatables.Count; i++)
            {
                _updatables.Add(_addUpdatables.Dequeue());
            }

            for (int i = 0; i < _removeUpdatables.Count; i++)
            {
                _updatables.Remove(_removeUpdatables.Dequeue());
            }
            
            for (var i = 0; i < _updatables.Count; i++)
            {
                if (_updatables[i].IsAlive)
                {
                    _updatables[i].UpdateLocal(deltaTime);
                }
            }
        }
    }
}
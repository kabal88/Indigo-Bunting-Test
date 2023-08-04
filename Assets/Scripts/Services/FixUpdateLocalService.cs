using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace Services
{
    public sealed class FixUpdateLocalService : IFixUpdateService
    {
        private readonly List<IFixUpdatable> _fixUpdatables;
        
        private Queue<IFixUpdatable> _addFixUpdatables;
        private Queue<IFixUpdatable> _removeFixUpdatables;
        
        public bool IsAlive { get; private set; }

        public FixUpdateLocalService()
        {
            _fixUpdatables = new List<IFixUpdatable>();
            _addFixUpdatables = new Queue<IFixUpdatable>();
            _removeFixUpdatables = new Queue<IFixUpdatable>();
            IsAlive = true;
        }

        public void RegisterObject(IFixUpdatable obj)
        {
            _addFixUpdatables.Enqueue(obj);
        }

        public void UnRegisterObject(IFixUpdatable obj)
        {
            _removeFixUpdatables.Enqueue(obj);
        }

        public IEnumerable<IFixUpdatable> GetObjectsByPredicate(Func<IFixUpdatable, bool> predicate)
        {
            return _fixUpdatables.Where(predicate);
        }
        
        public void FixedUpdateLocal()
        {
            for (var i = 0; i < _addFixUpdatables.Count; i++)
            {
                _fixUpdatables.Add(_addFixUpdatables.Dequeue());
            }

            for (var i = 0; i < _removeFixUpdatables.Count; i++)
            {
                _fixUpdatables.Remove(_removeFixUpdatables.Dequeue());
            }

            for (var i = 0; i < _fixUpdatables.Count; i++)
            {
                if (_fixUpdatables[i].IsAlive)
                {
                    _fixUpdatables[i].FixedUpdateLocal();
                }
            }
        }
    }
}
using Services;
using UnityEngine;

namespace Views
{
    public class LevelHolder : MonoBehaviour
    {
        [SerializeField] private SpawnPointView[] _spawnPoints;
        
        
        public void Init()
        {
            var points= GetComponentsInChildren<SpawnPointView>();
            var service = ServiceLocator.Get<SpawnService>();
            foreach (var point in points)
            {
                point.Init();
                service.RegisterObject(point);
            }
            _spawnPoints = points;
        }

        public void OnDestroy()
        {
            var service = ServiceLocator.Get<SpawnService>();
            foreach (var s in _spawnPoints)
            {
                service.UnRegisterObject(s);
            }
            _spawnPoints = null;
        }
    }
}
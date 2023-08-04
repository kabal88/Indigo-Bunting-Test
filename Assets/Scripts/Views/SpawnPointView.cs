using Data;
using Identifier;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Views
{
    public class SpawnPointView : MonoBehaviour, ISpawnPoint
    {
        [SerializeField] private SpawnPointIdentifier _id;
        [SerializeField] private bool _useManualParent;
        [SerializeField, ShowIf(nameof(_useManualParent))] private Transform _parent;

#if UNITY_EDITOR
        [SerializeField] private Color _color = new(0, 1, 0, 0.7f);
        private float _radius = 0.1f;
#endif


        public Transform Parent => _useManualParent ? _parent : transform;
        public SpawnData Data { get; private set; }


        public void Init()
        {
            Data = new SpawnData
            {
                Position = transform.position,
                Rotation = transform.rotation,
                Id = _id.Id
            };
        }

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            Handles.color = _color;
            var position = transform.position;
            Handles.DrawSolidDisc(position, Vector3.up, _radius);
            GUI.color = Color.black;
            var style = new GUIStyle
            {
                alignment = TextAnchor.LowerCenter,
                fontStyle = FontStyle.Bold
            };
            Handles.Label(position, _id.name, style);
        }
    }
#endif
}
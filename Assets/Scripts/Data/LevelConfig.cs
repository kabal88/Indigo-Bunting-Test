using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "BluePrints/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private float _order;
    [SerializeField, AssetsOnly] private GameObject _environmentPrefab;

    public float Order => _order;
    public GameObject EnvironmentPrefab => _environmentPrefab;
}
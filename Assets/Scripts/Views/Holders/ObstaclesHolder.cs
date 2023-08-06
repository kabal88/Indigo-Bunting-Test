using UnityEngine;

public class ObstaclesHolder : MonoBehaviour
{
    [SerializeField] private ObstaclesTag[] _obstacles;

    public void Awake()
    {
        _obstacles = GetComponentsInChildren<ObstaclesTag>();
    }
}

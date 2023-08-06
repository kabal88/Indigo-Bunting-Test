using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
#if UNITY_EDITOR
    public int Quantity = 10;
    public GameObject ObstaclePrefab;
    public float Radius = 10;
    public Transform GenerationCenter;
    public Transform GenerationCorner;
    public bool DeleteAllObstaclesBeforeGenerate = true;
    public float Size = 2;
    public float Offset = 0.2f;
    public Vector3 Dimension;


    [Button(ButtonSizes.Medium)]
    private void GenerateObstaclesInSphere()
    {
        var holder = GetComponentInChildren<ObstaclesHolder>();

        if (DeleteAllObstaclesBeforeGenerate)
        {
            foreach (Transform child in holder.transform)
            {
                if (child.TryGetComponent(out ObstaclesTag _))
                {
                    DestroyImmediate(child.gameObject);
                }
            }
        }

        for (int i = 0; i < Quantity; i++)
        {
            var randomPosition = Random.insideUnitSphere * Radius;
            randomPosition += GenerationCenter.position;

            var obj = PrefabUtility.InstantiatePrefab(ObstaclePrefab, holder.transform) as GameObject;
            obj.transform.position = randomPosition;
        }
    }

    [Button(ButtonSizes.Medium)]
    private void GenerateObstaclesInRect()
    {
        var holder = GetComponentInChildren<ObstaclesHolder>();

        if (DeleteAllObstaclesBeforeGenerate)
        {
            foreach (Transform child in holder.transform)
            {
                if (child.TryGetComponent(out ObstaclesTag _))
                {
                    DestroyImmediate(child.gameObject);
                }
            }
        }

        for (int x = 0; x < Dimension.x; x++)
        {
            var posX = (Size + Offset) * x;
            for (int y = 0; y < Dimension.y; y++)
            {
                var posY = (Size + Offset) * y;
                for (int z = 0; z < Dimension.z; z++)
                {
                    var posZ = (Size + Offset) * z;
                    var obj = PrefabUtility.InstantiatePrefab(ObstaclePrefab, holder.transform) as GameObject;
                    
                    obj.transform.position = GenerationCorner.position + new Vector3(posX, posY, posZ);
                }
            }
        }
    }
    
    
    
    public void OnDrawGizmos()
    {
        var color = new Color(1, 0.8f, 0.4f, 1);
        Handles.color = color;
        Handles.DrawWireDisc(GenerationCenter.position, Vector3.up, Radius);
        Handles.DrawWireDisc(GenerationCenter.position, Vector3.left, Radius);
        Handles.DrawWireDisc(GenerationCenter.position, Vector3.forward, Radius);
        // display object "value" in scene
        GUI.color = color;
        Handles.Label(GenerationCenter.position, "Generation Area");
        Handles.Label(GenerationCorner.position, "Generation Corner");
        
    }

#endif
}
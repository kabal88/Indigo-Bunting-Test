using System.Collections;
using System.Collections.Generic;
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
    public bool DeleteAllObstaclesBeforeGenerate = true;


    [Button(ButtonSizes.Medium)]
    private void GenerateObstacles()
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

            var obj = Instantiate(ObstaclePrefab, holder.transform);
            obj.transform.position = randomPosition;
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
    }

#endif
}
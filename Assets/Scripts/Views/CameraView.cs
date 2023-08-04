using UnityEngine;

public class CameraView : MonoBehaviour
{
    public Camera Camera { get; private set; }

    public void Init()
    {
        Camera = GetComponentInChildren<Camera>();
    }
}

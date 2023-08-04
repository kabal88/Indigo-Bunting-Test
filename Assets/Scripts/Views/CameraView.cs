using UnityEngine;

public class CameraView : MonoBehaviour
{
    public Camera Camera { get; private set; }
    public UnitHolderMonoComponent UnitHolder { get; private set; }
    
    public void Init()
    {
        Camera = GetComponentInChildren<Camera>();
        UnitHolder = GetComponentInChildren<UnitHolderMonoComponent>();
    }
}

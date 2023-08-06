using Interfaces;
using UnityEngine;
using Views;

public class BoneTag : MonoBehaviour,ITarget
{
    public bool IsAlive { get; }
    public UnitView Owner { get; private set; }
    
    public void SetOwner(UnitView owner)
    {
        Owner = owner;
    }
}

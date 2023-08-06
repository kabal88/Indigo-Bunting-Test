using UnityEngine;
using Views;

[RequireComponent(typeof(Rigidbody))]
public class BoneTag : MonoBehaviour
{
    public UnitView Owner { get; private set; }
    
    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void SetOwner(UnitView owner)
    {
        Owner = owner;
    }
}
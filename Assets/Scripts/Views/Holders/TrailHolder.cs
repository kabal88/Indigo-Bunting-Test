using UnityEngine;

public class TrailHolder : MonoBehaviour
{
    [SerializeField] private ParticleSystem _trail;
    
    public void SetTrailActive(bool value)
    {
        if (value)
        {
            _trail.Play(true);
        }
        else
        {
            _trail.Stop(true);
        }
    }
}

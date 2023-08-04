using UnityEngine;

namespace Views
{
    public class BaseParticleAnimation : MonoBehaviour
    {
        [SerializeField] protected ParticleSystem _attackParticles;

        public virtual void Play()
        {
            _attackParticles.Play();
        }
    }
}
using UnityEngine;

namespace Interfaces
{
    public interface ITarget
    {
        void SetRagdoll(bool isOn);

        void AddForce(Vector3 force);
    }
}
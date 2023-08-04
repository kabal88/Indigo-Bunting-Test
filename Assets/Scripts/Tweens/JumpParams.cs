using System;
using UnityEngine;

namespace Tweens
{
    [Serializable]
    public class JumpParams : TweenParams
    {
        public Vector3 EndValue;
        public float JumpPower;
        public int NumJumps;
        public bool Snapping = false;
    }
}
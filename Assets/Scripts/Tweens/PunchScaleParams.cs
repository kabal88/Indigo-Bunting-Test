using System;
using UnityEngine;

namespace Tweens
{
    [Serializable]
    public sealed class PunchScaleParams:TweenParams
    {
        public Vector3 Punch;
        public int Vibrato;
        public float Elasticity;
    }
}
using System;
using UnityEngine;

namespace Tweens
{
    [Serializable]
    public class ShakeParams : TweenParams
    {
        public Vector3 Strength;
        public int Vibrato;
        [Range(0, 180)] public float Randomness;
        public bool Snapping;
        public bool FadeOut = true;
    }
}
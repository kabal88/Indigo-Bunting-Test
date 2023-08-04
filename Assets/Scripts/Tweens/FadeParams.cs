using System;
using UnityEngine;

namespace Tweens
{
    [Serializable]
    public sealed class FadeParams: TweenParams
    {
        [Range(0.0f, 1.0f)] public float StartFade;
        [Range(0.0f, 1.0f)] public float TargetFade;
    }
}

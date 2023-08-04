using System;
using DG.Tweening;


namespace Tweens
{
    [Serializable]
    public class TweenParams
    {
        public float Duration;
        public float Delay;
        public Ease Ease = Ease.OutQuad;
    }
}

using DG.Tweening;
using Tweens;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class GlowWidget : MonoBehaviour
    {
        [SerializeField] private FadeParams _fadeParams;
        [SerializeField] private TweenLoopParams _loopParams;
        
        private Image _glowImage;
        private Sequence _sequence;

        private void Awake()
        {
            _glowImage = GetComponent<Image>();
        }

        public void Glow(bool isOn)
        {
            gameObject.SetActive(isOn);
            if (isOn)
            {
                _sequence = DOTween.Sequence();
                _sequence.Append(_glowImage.DOFade(_fadeParams.TargetFade, _fadeParams.Duration))
                    .Append(_glowImage.DOFade(_fadeParams.StartFade,_fadeParams.Duration))
                    .SetLoops(_loopParams.Loops,_loopParams.Type);
            }
            else
            {
                _sequence.Kill();
                _glowImage.DOFade(_fadeParams.StartFade, 0);
            }
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
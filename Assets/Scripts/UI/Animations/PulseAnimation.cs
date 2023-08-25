using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    [RequireComponent(typeof(RectTransform))]
    public class PulseAnimation : MonoBehaviour
    {
        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();

            _rectTransform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f)
                .SetEase(Ease.InOutSine).
                SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy() =>
            _rectTransform.DOKill();
    }
}

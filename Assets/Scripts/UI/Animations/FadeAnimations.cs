using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Animations
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeAnimations : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        private void Awake() =>
            _canvasGroup = GetComponent<CanvasGroup>();

        private void OnEnable()
        {
            _canvasGroup.alpha = 0;

            FadeIn();
        }

        public void FadeIn() =>
            _canvasGroup.DOFade(1, 0.7f);

        public void FadeOut() =>
            _canvasGroup.DOFade(0, 0.7f);
    }
}

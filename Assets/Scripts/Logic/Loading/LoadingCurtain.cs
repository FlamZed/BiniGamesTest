using DG.Tweening;
using UnityEngine;

namespace Logic.Loading
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        public CanvasGroup curtain;

        private void OnDestroy() =>
            curtain.DOKill();

        public void Show()
        {
            gameObject.SetActive(true);
            curtain.DOFade(1, 0.2f);
        }

        public void Hide()
        {
            curtain.DOFade(0, 0.3f).OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}

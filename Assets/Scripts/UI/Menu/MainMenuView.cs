using Infrastructure.Services.Audio;
using Infrastructure.Services.Audio.Type;
using Infrastructure.Services.Menu;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Menu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;

        [Inject] private IMainMenuService _mainMenuService;
        [Inject] private IAudioService _audioService;

        private void Start()
        {
            if (_playButton)
                _playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnDestroy()
        {
            if (_playButton)
                _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            _audioService.PlayOneShot(AudioClipShot.Tap);
            _mainMenuService.OnPlayClicked();
        }
    }
}

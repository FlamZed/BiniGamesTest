using System;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Audio.Type;
using Zenject;

namespace Infrastructure.Services.Level
{
    public class GameResetService : IGameResetService
    {
        [Inject] private IAudioService _audioService;

        public event Action OnRestart;
        public void RestartLevel()
        {
            _audioService.PlayOneShot(AudioClipShot.Kick);
            OnRestart?.Invoke();
        }
    }
}

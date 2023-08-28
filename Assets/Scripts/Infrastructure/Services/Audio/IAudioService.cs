using Infrastructure.Services.Audio.Type;
using UnityEngine;

namespace Infrastructure.Services.Audio
{
    public interface IAudioService
    {
        void PlayOneShot(AudioClip clip);
        void PlayOneShot(AudioClipShot clip);
        void PlayBackground(AudioClip clip);
        void PlayBackground(BackgroundClip clip);
    }
}

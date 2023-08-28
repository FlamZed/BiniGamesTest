using System;
using DG.Tweening;
using Infrastructure.AssetManagement;
using Infrastructure.Services.Audio.Type;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Audio
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField] private AudioSource _oneShotSource;
        [SerializeField] private AudioSource _backSource;

        [Inject] private IAssets _assets;

        public void PlayOneShot(AudioClipShot clip) =>
            PlayOneShot(ChoseShotClip(clip));

        public void PlayOneShot(AudioClip clip) =>
            _oneShotSource.PlayOneShot(clip);

        public void PlayBackground(BackgroundClip clip) =>
            PlayBackground(ChoseClip(clip));

        public void PlayBackground(AudioClip clip)
        {
            _backSource.DOFade(0, 0.5f).OnComplete(() =>
            {
                _backSource.clip = clip;
                _backSource.Play();
                _backSource.DOFade(1, 0.5f);
            });
        }

        private AudioClip ChoseShotClip(AudioClipShot clip) =>
            clip switch
            {
                AudioClipShot.Kick => _assets.Load<AudioClip>(AssetPath.KickClip),
                AudioClipShot.Tap => _assets.Load<AudioClip>(AssetPath.TapClip),
                AudioClipShot.Win => _assets.Load<AudioClip>(AssetPath.WinClip),
                AudioClipShot.Lose => _assets.Load<AudioClip>(AssetPath.LoseClip),
                _ => throw new ArgumentOutOfRangeException()
            };

        private AudioClip ChoseClip(BackgroundClip clip) =>
            clip switch
            {
                BackgroundClip.Menu => _assets.Load<AudioClip>(AssetPath.BackgroundMenuClip),
                BackgroundClip.Game => _assets.Load<AudioClip>(AssetPath.BackgroundClip),
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}

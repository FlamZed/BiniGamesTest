using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Factory;
using Infrastructure.Services.Audio;
using Logic.Player;
using UnityEngine;
using Utility;
using Zenject;

namespace Infrastructure.Services.Score
{
    public class ScoreService : IScoreService
    {
        public event Action OnWin;
        public event Action OnLose;

        [Inject] IGameFactory _gameFactory;
        [Inject] private IAudioService _audioService;

        private List<MergeBehavior> _ballsMergeBehaviors = new();

        private BallsTable _chasConfig;

        private bool _finalized = false;

        public void Init(BallsTable config)
        {
            _chasConfig = config;

            _finalized = false;
        }

        public void RegisterPlayer(MergeBehavior configSetter)
        {
            if (_finalized)
                return;

            if (_ballsMergeBehaviors.Count >= 30)
            {
                _finalized = true;
                OnLose?.Invoke();

                _ballsMergeBehaviors.Clear();
                return;
            }

            _ballsMergeBehaviors.Add(configSetter);

            configSetter.OnDestroyed += RemoveBall;
        }

        private void RemoveBall(MergeBehavior first, MergeBehavior second)
        {
            RemoveBall(first);
            RemoveBall(second);

            OnIncrementChanged(first, first.transform.transform.position);
        }

        public void GetRecommendedBall(out Ball ball)
        {
            if (_ballsMergeBehaviors.Count < 3)
                _chasConfig.GetRandomBall(3, out ball);
            else
                _chasConfig.GetRandomBall(_ballsMergeBehaviors.Max(x => x.GetConfig().GetIndex()), out ball);
        }

        private void RemoveBall(MergeBehavior mergeBehavior)
        {
            _ballsMergeBehaviors.Remove(mergeBehavior);

            mergeBehavior.OnDestroyed -= RemoveBall;
        }

        private void OnIncrementChanged(MergeBehavior configSetter, Vector3 position)
        {
            _audioService.PlayOneShot(AudioClipShot.Kick);
            
            var info = configSetter.GetConfig();

            var index = 1 + info.GetIndex();

            Debug.LogWarning(index);

            if (_chasConfig.IsMaxValue(index))
                _finalized = true;

            _gameFactory.CreateHero(_chasConfig.GetIncrementedBall(index), at: position, isPlayer: false);

            if (_finalized)
                FinisLevel(configSetter);
        }

        private void FinisLevel(MergeBehavior mergeBehavior)
        {
#if UNITY_EDITOR
            Debug.LogWarning("Win");
#endif

            RemoveBall(mergeBehavior);

            _ballsMergeBehaviors.ForEach(behavior =>
            {
                behavior.OnDestroyed -= RemoveBall;

                behavior.MoveToAndDestroy(mergeBehavior.gameObject.transform);
            });

            _ballsMergeBehaviors.Clear();

            OnWin?.Invoke();
        }
    }
}

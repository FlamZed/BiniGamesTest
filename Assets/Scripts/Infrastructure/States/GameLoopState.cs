using System.Collections;
using Infrastructure.Factory;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Input;
using Infrastructure.Services.Level;
using Infrastructure.Services.Score;
using UnityEngine;
using Utility;
using Zenject;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        private readonly IScoreService _scoreService;
        private readonly IInputService _inputService;
        private readonly IGameFactory _gameFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ISpawnPointService _spawnPoint;
        private readonly IGameResetService _gameResetService;
        private readonly IAudioService _audioService;

        private bool _isReleased = false;

        private Vector3 _spawnPointPosition;

        public GameLoopState(GameStateMachine gameStateMachine, DiContainer diContainer)
        {
            _gameStateMachine = gameStateMachine;

            _scoreService = diContainer.Resolve<IScoreService>();
            _inputService = diContainer.Resolve<IInputService>();
            _gameFactory = diContainer.Resolve<IGameFactory>();
            _coroutineRunner = diContainer.Resolve<ICoroutineRunner>();
            _spawnPoint = diContainer.Resolve<ISpawnPointService>();
            _gameResetService = diContainer.Resolve<IGameResetService>();
            _audioService = diContainer.Resolve<IAudioService>();
        }

        public void Enter()
        {
            _inputService.OnRelease += OnRelease;

            _scoreService.OnWin += ToWinSate;
            _scoreService.OnLose += ToLoseState;

            _gameResetService.OnRestart += ToRestart;

            _spawnPointPosition = _spawnPoint.GetSpawnPoint;

            _audioService.PlayBackground(BackgroundClip.Game);
        }

        private void ToRestart() =>
            _gameStateMachine.Enter<LoadMenuState>();

        public void Exit()
        {
            _inputService.OnRelease -= OnRelease;

            _scoreService.OnWin -= ToWinSate;
            _scoreService.OnLose -= ToLoseState;

            _gameResetService.OnRestart -= ToRestart;
        }

        private void ToWinSate()
        {
            _audioService.PlayOneShot(AudioClipShot.Win);
            Debug.LogWarning("Win");
        }

        private void ToLoseState()
        {
            _audioService.PlayOneShot(AudioClipShot.Lose);
            Debug.LogWarning("Lose");
        }

        private void OnRelease()
        {
            if (!_isReleased)
                _coroutineRunner.Run(WaitDelay());

            _isReleased = true;
        }

        private IEnumerator WaitDelay()
        {
            _scoreService.GetRecommendedBall(out Ball ball);

            yield return new WaitForSeconds(0.5f);

            _gameFactory.CreateHero(ball, at: _spawnPointPosition, true);

            _isReleased = false;
        }
    }
}

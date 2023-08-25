using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services.Level;
using Infrastructure.Services.Score;
using Logic.Loading;
using UnityEngine;
using Utility;
using Zenject;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private IGameStateMachine _stateMachine;

        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IAssets _assetProvider;
        private readonly IScoreService _scoreService;
        private readonly ISpawnPointService _spawnPoint;

        private BallsTable _config;

        public LoadLevelState(GameStateMachine stateMachine, DiContainer diContainer)
        {
            _stateMachine = stateMachine;

            _assetProvider = diContainer.Resolve<IAssets>();
            _gameFactory = diContainer.Resolve<IGameFactory>();
            _sceneLoader = diContainer.Resolve<ISceneLoader>();
            _curtain = diContainer.Resolve<ILoadingCurtain>();
            _scoreService = diContainer.Resolve<IScoreService>();
            _spawnPoint = diContainer.Resolve<ISpawnPointService>();
        }

        public void Enter(string sceneName)
        {
            _spawnPoint.AllowSpawnPoint += SpawnHero;

            _curtain.Show();

            LoadConfig();

            _sceneLoader.Load(sceneName, onLoaded: OnLoaded);
        }

        private void LoadConfig() =>
            _config = _assetProvider.Load<BallsTable>(AssetPath.BallsTable);

        private void OnLoaded()
        {
            InitGameWorld();

            _stateMachine.Enter<GameLoopState>();
        }

        private void SpawnHero(Vector3 pos)
        {
            _config.GetRandomBall(2, out var ball);

            _gameFactory.CreateHero(ball, pos, true);
        }

        private void InitGameWorld()
        {
            _scoreService.Init(_config);
        }

        public void Exit()
        {
            _spawnPoint.AllowSpawnPoint -= SpawnHero;

            _curtain.Hide();
        }
    }
}

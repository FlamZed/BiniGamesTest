using Infrastructure.AssetManagement;
using Infrastructure.DIContainer.Extensions;
using Infrastructure.Factory;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Input;
using Infrastructure.Services.Level;
using Infrastructure.Services.Menu;
using Infrastructure.Services.Score;
using Infrastructure.States;
using Logic.Loading;
using UnityEngine;
using Zenject;

namespace Infrastructure.DIContainer.ProjectInstallers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private GameObject _curtainPrefab;
        [SerializeField] private GameObject _coroutineRunner;
        [SerializeField] private GameObject _audioService;
        [SerializeField] private GameObject _inputService;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle().NonLazy();

            Container.BindService<ICoroutineRunner, CoroutineRunner>(_coroutineRunner);
            Container.BindService<ILoadingCurtain, LoadingCurtain>(_curtainPrefab);

            Container.BindService<ISceneLoader, SceneLoader>();
            Container.BindService<IAssets, AssetProvider>();
            Container.BindService<IGameFactory, GameFactory>();

            Container.BindService<IMainMenuService, MainMenuService>();

            Container.BindService<IAudioService, AudioService>(_audioService);

            Container.BindService<IInputService, InputService>(_inputService);

            Container.BindService<IScoreService, ScoreService>();

            Container.BindService<ISpawnPointService, SpawnPointService>();
            Container.BindService<IGameResetService, GameResetService>();

            Container.Bind<Game>().AsSingle().NonLazy();
        }

        public void Initialize()
        {
            Debug.Log("BootstrapInstaller.Initialize()");

            Container.Resolve<Game>().StateMachine.Enter<BootstrapState>();
        }
    }
}

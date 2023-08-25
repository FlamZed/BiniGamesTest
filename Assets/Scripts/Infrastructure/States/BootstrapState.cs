using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Boot";

        private readonly GameStateMachine _gameStateMachine;

        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, DiContainer container)
        {
            _gameStateMachine = gameStateMachine;

            _sceneLoader = container.Resolve<ISceneLoader>();
        }

        public void Enter()
        {
            if (SceneManager.GetActiveScene().name != Initial)
                _sceneLoader.Load(Initial, onLoaded: LoadEnterState);
            else
                LoadEnterState();
        }

        private void LoadEnterState() =>
            _gameStateMachine.Enter<LoadMenuState>();

        public void Exit()
        {
        }
    }
}

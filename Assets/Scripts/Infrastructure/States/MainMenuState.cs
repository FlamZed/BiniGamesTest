using Infrastructure.Services;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Menu;
using Zenject;

namespace Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly GameStateMachine _stateMachine;

        private readonly IMainMenuService _mainMenuService;
        private readonly IAudioService _audioService;

        public MainMenuState(GameStateMachine stateMachine, DiContainer diContainer)
        {
            _stateMachine = stateMachine;

            _mainMenuService = diContainer.Resolve<IMainMenuService>();
            _audioService = diContainer.Resolve<IAudioService>();
        }

        public void Exit()
        {
            _mainMenuService.LoadLevel -= LoadLevelState;
        }

        public void Enter()
        {
            _audioService.PlayBackground(BackgroundClip.Menu);

            _mainMenuService.LoadLevel += LoadLevelState;
        }

        private void LoadLevelState() =>
            _stateMachine.Enter<LoadLevelState, string>("Level");
    }
}

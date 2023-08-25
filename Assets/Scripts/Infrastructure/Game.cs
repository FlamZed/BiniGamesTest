using Infrastructure.States;
using Zenject;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(DiContainer diContainer)
        {
            StateMachine = new GameStateMachine(diContainer);
        }
    }
}

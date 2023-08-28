using System;

namespace Infrastructure.Services.Level
{
    public interface IGameResetService
    {
        event Action OnRestart;
        void RestartLevel();
    }
}
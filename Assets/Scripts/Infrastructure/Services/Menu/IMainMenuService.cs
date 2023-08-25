using System;

namespace Infrastructure.Services.Menu
{
    public interface IMainMenuService
    {
        event Action LoadLevel;
        void OnPlayClicked();
    }
}

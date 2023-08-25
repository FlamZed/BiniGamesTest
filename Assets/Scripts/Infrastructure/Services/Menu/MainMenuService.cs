using System;

namespace Infrastructure.Services.Menu
{
    public class MainMenuService : IMainMenuService
    {
        public event Action LoadLevel;

        public void OnPlayClicked()
        {
            LoadLevel?.Invoke();
        }
    }
}

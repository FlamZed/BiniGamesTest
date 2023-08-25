using System;
using System.Collections;

namespace Infrastructure
{
    public interface ISceneLoader
    {
        void Load(string name, Action onLoaded = null);
        IEnumerator LoadScene(string name, Action onLoaded);
        event Action OnSceneLoaded;
    }
}

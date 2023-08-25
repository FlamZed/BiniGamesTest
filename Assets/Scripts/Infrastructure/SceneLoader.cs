using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public event Action OnSceneLoaded;

        public SceneLoader(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null) =>
            _coroutineRunner.Run(LoadScene(name, onLoaded: onLoaded));

        public IEnumerator LoadScene(string name, Action onLoaded)
        {
            Debug.Log("Loading scene: " + name + "...");

            if (SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();

                OnSceneLoaded?.Invoke();

                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

            while (!waitNextScene.isDone)
                yield return null;

            onLoaded?.Invoke();

            OnSceneLoaded?.Invoke();
        }
    }
}

using System;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Input
{
    public class InputService : MonoBehaviour, IInputService
    {
        public event Action OnTap;
        public event Action OnRelease;

        private Camera _camera;

        [Inject] private ISceneLoader _sceneLoader;

        private void Start() =>
            _sceneLoader.OnSceneLoaded += UpdateCamera;

        private void OnDestroy() =>
            _sceneLoader.OnSceneLoaded -= UpdateCamera;

        public void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
                OnTap?.Invoke();
            else if (UnityEngine.Input.GetKeyUp(KeyCode.Mouse0))
                OnRelease?.Invoke();
        }

        public void GetMousePosition(out Vector3 position) =>
            position = _camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition);

        private void UpdateCamera() =>
            _camera = Camera.main;
    }
}

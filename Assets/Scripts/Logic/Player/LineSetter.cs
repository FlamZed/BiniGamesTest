using System;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class LineSetter : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;

        private IInputService _inputService;

        private bool _isDrawing;

        public void Init(IInputService inputService)
        {
            _inputService = inputService;

            _lineRenderer.enabled = false;

            _inputService.OnTap += DrawLine;
            _inputService.OnRelease += HideLine;
        }

        private void Update()
        {
            if (_inputService == null)
                return;

            if (!_isDrawing)
                return;

            _inputService.GetMousePosition(out var mousePosition);

            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, -1f));
            _lineRenderer.SetPosition(1, new Vector3(mousePosition.x, mousePosition.y, -1f));
        }

        private void OnDestroy()
        {
            _inputService.OnTap -= DrawLine;
            _inputService.OnRelease -= HideLine;
        }

        private void DrawLine()
        {
            _lineRenderer.enabled = true;
            _isDrawing = true;
        }

        private void HideLine()
        {
            _lineRenderer.enabled = false;
            _isDrawing = false;

            _inputService.OnTap -= DrawLine;
            _inputService.OnRelease -= HideLine;
        }
    }
}

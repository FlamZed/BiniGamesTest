using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class PlayerImpulse : MonoBehaviour
    {
        [Inject] private IInputService _inputService;

        private Rigidbody2D _rigidbody;

        public void Init(IInputService inputService) =>
            _inputService = inputService;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _inputService.OnRelease += SetImpulse;
        }

        private void SetImpulse()
        {
            if (_rigidbody == null)
                return;

            _inputService.OnRelease -= SetImpulse;

            _rigidbody.simulated = true;
            _rigidbody.velocity = Vector3.zero;

            _inputService.GetMousePosition(out var mousePosition);

            Vector3 direction = mousePosition - transform.position;

            direction.Normalize();

            _rigidbody.AddForce(direction * 50, ForceMode2D.Impulse);
        }
    }
}

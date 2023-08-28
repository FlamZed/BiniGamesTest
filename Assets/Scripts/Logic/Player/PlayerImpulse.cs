using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Logic.Player
{
    public class PlayerImpulse : MonoBehaviour
    {
        [SerializeField] private int _force;
        [SerializeField] private float _maxDirectionMagnitude;

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

            direction = Vector3.ClampMagnitude(direction, _maxDirectionMagnitude);

            _rigidbody.AddForce(direction * _force, ForceMode2D.Impulse);
        }
    }
}

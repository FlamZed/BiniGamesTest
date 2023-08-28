using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic.Player
{
    public class MergeBehavior : MonoBehaviour
    {
        public event Action<MergeBehavior, MergeBehavior> OnDestroyed;

        public PlayerView GetModel() => playerView;

        [SerializeField] private MergeCollider _mergeCollider;

        [SerializeField] private PlayerView playerView;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private float _distance = 0.2f;
        [SerializeField] private float _mergeSpeed = 0.1f;

        [SerializeField] private GameObject _explosionPrefab;

        private bool _canMerge;

        private Transform _firstBall;
        private Transform _secondBall;

        private MergeBehavior _secondConfig;

        private void OnEnable()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _mergeCollider.OnCollide += SpringAndDestroy;
        }

        private void FixedUpdate()
        {
            if (_canMerge)
            {
                transform.position = Vector2.MoveTowards(_firstBall.position, _secondBall.position, _mergeSpeed);
                if (Vector2.Distance(_firstBall.position, _secondBall.position) < _distance)
                {
                    OnDestroyed?.Invoke(this, _secondConfig);

                    _canMerge = false;

                    SpawnExplosionPrefab();

                    Destroy(_secondBall.gameObject);
                    Destroy(gameObject);

                }
            }
        }

        private void SpawnExplosionPrefab()
        {
            var effect = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(effect, 0.5f);
        }

        private void OnDestroy()
        {
            _mergeCollider.OnCollide -= SpringAndDestroy;
            transform.DOKill();
        }

        public void SpringAndDestroy(GameObject anotherGO)
        {
            if (anotherGO.TryGetComponent(out MergeBehavior config))
            {
                if(playerView.GetIndex() != config.GetModel().GetIndex())
                    return;

                _secondConfig = config;

                if (anotherGO.TryGetComponent(out Rigidbody2D anotherBall))
                {
                    Destroy(_rigidbody2D);
                    Destroy(anotherBall);

                    _firstBall = transform;
                    _secondBall = anotherGO.transform;

                    _canMerge = true;
                }
            }
            else
            {
                throw new AggregateException("PlayerConfigSetter not found");
            }
        }

        public void MoveToAndDestroy(Transform position)
        {
            Destroy(_rigidbody2D);

            transform.DOMove(position.position, 0.5f)
                .SetEase(Ease.OutBack).OnComplete(() => Destroy(gameObject));
        }
    }
}

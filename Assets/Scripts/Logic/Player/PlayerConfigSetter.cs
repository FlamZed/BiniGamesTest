﻿using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Utility;

namespace Logic.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerConfigSetter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TMP_Text _textIncrement;

        [SerializeField] private TrailRenderer _trailRenderer;

        [SerializeField] private GameObject _explosionPrefab;

        public int GetIndex() => _config.Index;

        private Ball _config;

        public void Init(Ball ball, bool isPlayer)
        {
            _config = ball;

            var effect = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(effect, 0.4f);

            SetScale(ball);

            _trailRenderer.colorGradient = ball.Gradient;

            if (_spriteRenderer == null)
            {
                if (!TryGetComponent(out _spriteRenderer))
                    throw new ArgumentException("SpriteRenderer not found");
            }

            if (_textIncrement == null)
            {
                if (!TryGetComponent(out _textIncrement))
                    throw new ArgumentException("TMP_Text not found");
            }

            UpdateInfo();

            if (!isPlayer)
            {
                GetComponent<Rigidbody2D>().simulated = true;

                Destroy(GetComponent<PlayerImpulse>());
                Destroy(GetComponent<LineSetter>());
            }
        }

        private void SetScale(Ball ball)
        {
            var scale = transform.localScale + Vector3.one * (0.1f * ball.Index);

            transform.localScale = scale / 3;
            transform.DOScale(scale, 0.5f).SetEase(Ease.OutBack);
        }

        private void UpdateInfo()
        {
            _spriteRenderer.sprite = _config.Sprite;
            _textIncrement.text = _config.Increment.ToString();
        }
    }
}

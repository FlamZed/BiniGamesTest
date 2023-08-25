using System;
using UnityEngine;

namespace Logic.Player
{
    public class MergeCollider : MonoBehaviour
    {
        public event Action<GameObject> OnCollide;

        private const string Player = "Player";

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(Player))
                return;

            OnCollide?.Invoke(other.gameObject);
        }
    }
}

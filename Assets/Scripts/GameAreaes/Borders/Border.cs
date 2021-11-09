using System;
using UnityEngine;

namespace GameAreaes.Borders
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Border : MonoBehaviour
    {
        public event Action<Collision2D> Collided;

        private BoxCollider2D _boxCollider2D;

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        public void Initialize(Vector2 size)
        {
            _boxCollider2D.size = size;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Collided?.Invoke(other);
        }
    }
}
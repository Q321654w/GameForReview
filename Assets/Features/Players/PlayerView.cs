using IDamageables;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour, IDamageable
    {
        private BoxCollider2D _boxCollider2D;
        private Player _player;

        private void Awake()
        {
            _boxCollider2D = GetComponent<BoxCollider2D>();
        }

        public void Initialize(Player player, Vector2 size)
        {
            _player = player;
            _boxCollider2D.size = size;
        }

        public void TakeDamage(int damage)
        {
            _player.TakeDamage(damage);
        }
    }
}
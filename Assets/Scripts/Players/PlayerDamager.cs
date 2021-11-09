using Balls;
using GameAreaes.Borders;
using UnityEngine;

namespace Players
{
    public class PlayerDamager
    {
        private Player _player;
        
        public PlayerDamager(Player player, Border border)
        {
            _player = player;
            border.Collided += OnCollided;
        }

        private void OnCollided(Collision2D collision2D)
        {
            if (!collision2D.gameObject.TryGetComponent(out Ball ball)) return;
            var damage = ball.Damage;
            _player.Health.TakeDamage(damage);
        }
    }
}
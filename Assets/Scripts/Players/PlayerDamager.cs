using Balls;
using Common;
using GameAreas.Borders;
using UnityEngine;

namespace Players
{
    public class PlayerDamager : ICleanUp
    {
        private readonly Player _player;
        private readonly Border _border;
        
        public PlayerDamager(Player player, Border border)
        {
            _player = player;
            _border = border;
            _border.Collided += OnCollided;
        }

        private void OnCollided(Collision2D collision2D)
        {
            if (!collision2D.gameObject.TryGetComponent(out Ball ball)) return;
            var damage = ball.Damage;
            _player.TakeDamage(damage);
        }

        void ICleanUp.CleanUp()
        {
            _border.Collided -= OnCollided;
        }
    }
}
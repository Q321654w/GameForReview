using System;
using GameAreaes;
using IDamageables;
using UnityEngine;
using UpdateCollections;

namespace Players
{
    public class Player : IDamageable, IGameUpdate
    {
        public event Action<IGameUpdate> UpdateRemoveRequested;
        
        public Health Health { get; }

        private readonly PlayerInput _playerInput;
        private readonly GameArea _gameArea;
        private readonly int _damage;

        public Player(PlayerInput playerInput, GameArea gameArea, int damage, Health health)
        {
            _damage = damage;
            _playerInput = playerInput;
            _gameArea = gameArea;
            _playerInput.Clicked += OnClicked;

            Health = health;
        }
        private void OnClicked(Vector2 mousePosition)
        {
            Attack(mousePosition);
        }
        private void Attack(Vector2 mousePosition)
        {
            var hit = _gameArea.GetObjectAt(mousePosition);

            if (hit.collider == null)
                return;

            if (hit.collider.TryGetComponent(out IDamageable damageable))
                damageable.Health.TakeDamage(_damage);
        }
        
        public void GameUpdate(float deltaTime)
        {
            _playerInput.Update(deltaTime);
        }
    }
}
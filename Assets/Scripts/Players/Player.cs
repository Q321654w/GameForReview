using System;
using Common;
using GameAreas;
using GameUpdate;
using IDamageables;
using UnityEngine;

namespace Players
{
    public class Player : IDamageable, IGameUpdate, ICleanUp
    {
        public event Action Died;
        public event Action<IGameUpdate> UpdateRemoveRequested;

        private readonly Health _health;

        private readonly PlayerInput _playerInput;
        private readonly GameArea _gameArea;
        private readonly int _damage;

        public Player(PlayerInput playerInput, GameArea gameArea, int damage, Health health)
        {
            _damage = damage;
            _playerInput = playerInput;
            _gameArea = gameArea;
            _playerInput.Clicked += OnClicked;

            _health = health;
            _health.Died += OnDied;
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
                damageable.TakeDamage(_damage);
        }
        
        public void GameUpdate(float deltaTime)
        {
            _playerInput.Update(deltaTime);
        }

        void ICleanUp.CleanUp()
        {
            _health.Died -= OnDied;
            _playerInput.Clicked -= OnClicked;
            UpdateRemoveRequested?.Invoke(this);
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }

        private void OnDied()
        {
            Died?.Invoke();
        }
    }
}
using System;
using Effects;
using GameUpdate;
using IDamageables;
using Movements;
using Pools;
using Scores;
using UnityEngine;

namespace Balls
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Ball : PooledObject, IDamageable, IScoreProvider, IGameUpdate
    {
        public event Action<IGameUpdate> UpdateRemoveRequested;

        public event Action<int> Scored;

        private SpriteRenderer _spriteRenderer;
        private Effect _effect;
        private Color _color;

        private Movement _movement;
        private Health _health;

        private int _killPoints;
        private int _damage;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(Health health, Movement movement, int killPoints, int damage, Effect effect, Color color)
        {
            _damage = damage;
            _movement = movement;
            _killPoints = killPoints;
            _effect = effect;

            _color = color;
            _spriteRenderer.color = _color;

            _health = health;
            _health.Died += OnDied;
        }

        private void OnDied()
        {
            PlayDieParticles();

            Scored?.Invoke(_killPoints);
            Scored = null;

            DisableSelf();
        }

        private void PlayDieParticles()
        {
            _effect.Play(transform, _color);
        }

        public void GameUpdate(float deltaTime)
        {
            _movement.Move(deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(_damage);
                DisableSelf();
            }
        }

        private void DisableSelf()
        {
            UpdateRemoveRequested?.Invoke(this);
            ReturnToPool();
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }
    }
}
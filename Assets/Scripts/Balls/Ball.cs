using System;
using DefaultNamespace;
using GameAreaes.Borders;
using IDamageables;
using Movements;
using Pools;
using Scores;
using UnityEngine;
using UpdateCollections;

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

        public int Damage { get; private set; }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(Health health, Movement movement, int killPoints, int damage, Effect effect, Color color)
        {
            Damage = damage;
            _health = health;
            _health.Died += OnDied;

            _movement = movement;
            _killPoints = killPoints;
            _effect = effect;
            _color = color;
            _spriteRenderer.color = _color;
        }
        private void OnDied()
        {
            PlayDieParticles();

            Scored?.Invoke(_killPoints);
            Scored = null;

            Disable();
        }
        private void PlayDieParticles()
        {
            _effect.Play(transform,_color);
        }

        public void GameUpdate(float deltaTime)
        {
            _movement.Move(deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<Border>(out _))
            {
                Disable();
            }
        }

        private void Disable()
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
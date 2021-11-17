using System;
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
        
        [SerializeField] private ParticleSystem _dieParticles;

        private SpriteRenderer _spriteRenderer;
        private Color _color;

        private Movement _movement;

        private int _killPoints;

        public Health Health { get; private set; }
        public int Damage { get; private set; }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(Health health, Movement movement, int killPoints, int damage, Color color)
        {
            Health = health;
            Health.Died += OnDied;

            _movement = movement;
            _killPoints = killPoints;
            _color = color;
            Damage = damage;
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
            var particles = Instantiate(_dieParticles, transform.position, Quaternion.identity);

            var particlesMain = particles.main;
            particlesMain.startColor = _color;
            particles.Play();
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
    }
}
using System;
using Ranges;
using UnityEngine;

namespace Balls.Stats
{
    [Serializable]
    public class BallStats
    {
        [SerializeField] private IntRange _killPoints;
        [SerializeField] private IntRange _damage;
        [SerializeField] private IntRange _hitPoints;
        [SerializeField] private FloatRange _speed;
        [SerializeField] private Color _color;

        public IntRange KillPoints => _killPoints;
        public IntRange Damage => _damage;
        public IntRange HitPoints => _hitPoints;
        public FloatRange Speed => _speed;
        public Color Color => _color;

        public BallStats(IntRange killPoints, IntRange damage, IntRange hitPoints, FloatRange speed, Color color)
        {
            _killPoints = killPoints;
            _damage = damage;
            _hitPoints = hitPoints;
            _speed = speed;
            _color = color;
        }

        public BallStats(float speed)
        {
            _speed = new FloatRange(speed, speed);
        }

        public static BallStats operator +(BallStats ballStats1, BallStats ballStats2)
        {
            var speed = ballStats1.Speed + ballStats2.Speed;
            var damage = ballStats1.Damage + ballStats2.Damage;
            var hitPoints = ballStats1.HitPoints + ballStats2.HitPoints;
            var killPoints = ballStats1.KillPoints + ballStats2.KillPoints;
            var color = ballStats1.Color + ballStats2.Color;
            return new BallStats(killPoints, damage, hitPoints, speed, color);
        }
    }
}
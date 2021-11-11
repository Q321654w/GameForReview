using System;
using Balls;
using Balls.Stats;
using Balls.Stats.Decorators;
using IDamageables;
using Movements;
using Movements.DirectionProviders;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BallGenerators.Builder
{
    public class BallBuilder
    {
        public event Action<Ball> Builded;

        private IBallStatsProvider _ballStatsProvider;
        private Ball _prefab;

        private BallStats Stats => _ballStatsProvider.Stats;

        public BallBuilder(IBallStatsProvider ballStatsProvider, Ball prefab)
        {
            _ballStatsProvider = ballStatsProvider;
            _prefab = prefab;
        }

        public Ball BuildBall()
        {
            var instance = Object.Instantiate(_prefab);
            InitializeBall(instance);
            Builded?.Invoke(instance);
            return instance;
        }

        public void InitializeBall(Ball ball)
        {
            var health = GetHealth();
            var movement = GetMovement(ball);
            var damage = Stats.Damage.GetRandomValue();
            var killPoints = Stats.KillPoints.GetRandomValue();

            ball.Initialize(health, movement, killPoints, damage, Stats.Color);
        }

        private Health GetHealth()
        {
            return new Health(Stats.HitPoints.GetRandomValue());
        }

        private Movement GetMovement(Component ball)
        {
            var directionProvider = new BallDirectionProvider();
            return new Movement(ball.transform, Stats.Speed.GetRandomValue(), directionProvider);
        }

        public void Dispose()
        {
            Builded = null;
        }
    }
}
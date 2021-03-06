using Balls;
using Balls.Stats;
using Balls.Stats.Decorators;
using Effects;
using IDamageables;
using Movements;
using Movements.DirectionProviders;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BallGenerators.Builder
{
    public class BallBuilder
    {
        private readonly IBallStatsProvider _ballStatsProvider;
        private readonly Ball _prefab;
        private readonly Effect _dieEffect;

        private BallStats Stats => _ballStatsProvider.Stats;

        public BallBuilder(IBallStatsProvider ballStatsProvider, Ball prefab, Effect dieEffect)
        {
            _ballStatsProvider = ballStatsProvider;
            _prefab = prefab;
            _dieEffect = dieEffect;
        }

        public Ball BuildBall()
        {
            var instance = Object.Instantiate(_prefab);
            InitializeBall(instance);
            return instance;
        }

        public void InitializeBall(Ball ball)
        {
            var health = GetHealth();
            var movement = GetMovement(ball);
            var damage = Stats.Damage.GetRandomValue();
            var killPoints = Stats.KillPoints.GetRandomValue();
            var color = Stats.ColorSequence.GetRandomColor();

            ball.Initialize(health, movement, killPoints, damage, _dieEffect, color);
        }

        private Health GetHealth()
        {
            return new Health(Stats.HitPoints.GetRandomValue());
        }

        private Movement GetMovement(Component ball)
        {
            var directionProvider = new ConstantDirectionProvider();
            return new Movement(ball.transform, Stats.Speed.GetRandomValue(), directionProvider);
        }
    }
}
using BallGenerators;
using BallGenerators.Builder;
using BallGenerators.Builder.Configs;
using Balls.Stats.Decorators;
using Balls.Stats.Decorators.Realizations;
using Common;
using GameAreas;
using Ranges;
using UnityEngine;

namespace Installers
{
    public class BallGeneratorInstaller : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _ballSpeedScale;
        [SerializeField] private BallConfig _ballConfig;
        [SerializeField] private FloatRange _spawnRateRange;

        public BallGenerator Install(GameArea gameArea, GameUpdates.GameUpdates gameUpdates)
        {
            return CreateBallGenerator(gameArea, gameUpdates);
        }

        private BallGenerator CreateBallGenerator(GameArea gameArea, GameUpdates.GameUpdates gameUpdates)
        {
            var ballProvider = CreateBallProvider(gameUpdates);

            var timer = new RandomLoopTimer(_spawnRateRange);
            var ballPlacer = new BallPlacer(gameArea);
            var ballGenerator = new BallGenerator(ballPlacer, ballProvider, timer);

            gameUpdates.AddToUpdateList(timer);
            return ballGenerator;
        }

        private BallProvider CreateBallProvider(GameUpdates.GameUpdates gameUpdates)
        {
            var statsProvider = CreateStatsProvider(gameUpdates);
            var dieEffect = _ballConfig.DieEffect;

            var ballBuilder = new BallBuilder(statsProvider, _ballConfig.Prefab, dieEffect);
            var ballProvider = new BallProvider(ballBuilder);

            return ballProvider;
        }

        private IBallStatsProvider CreateStatsProvider(GameUpdates.GameUpdates gameUpdates)
        {
            var stopwatch = new Stopwatch();
            var statsProvider = new TimeScalingSpeed(_ballConfig, _ballSpeedScale, stopwatch);

            gameUpdates.AddToUpdateList(stopwatch);

            return statsProvider;
        }
    }
}
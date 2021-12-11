using BallGenerators;
using BallGenerators.Builder;
using BallGenerators.Builder.Configs;
using Balls.Stats.Decorators;
using Balls.Stats.Decorators.Realizations;
using GameAreaes;
using UnityEngine;
using UpdateCollections;

namespace DefaultNamespace.Installers
{
    public class BallGeneratorInstaller : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _ballSpeedScale;
        [SerializeField] private BallConfig _ballConfig;
        [SerializeField] private float _spawnRate;

        public BallGenerator Install(GameArea gameArea, GameUpdates gameUpdates)
        {
            return CreateBallGenerator(gameArea,gameUpdates);
        }

        private BallGenerator CreateBallGenerator(GameArea gameArea, GameUpdates gameUpdates)
        {
            var ballProvider = CreateBallProvider(gameUpdates);

            var ballPlacer = new BallPlacer(gameArea);
            var ballGenerator = new BallGenerator(ballPlacer, ballProvider, _spawnRate);
            
            gameUpdates.AddToUpdateList(ballGenerator);
            return ballGenerator;
        }

        private BallProvider CreateBallProvider(GameUpdates gameUpdates)
        {
            var statsProvider = CreateStatsProvider(gameUpdates);
            var dieEffect = _ballConfig.DieEffect;

            var ballBuilder = new BallBuilder(statsProvider, _ballConfig.Prefab, dieEffect);
            var ballProvider = new BallProvider(ballBuilder);

            return ballProvider;
        }

        private IBallStatsProvider CreateStatsProvider(GameUpdates gameUpdates)
        {
            var stopwatch = new Stopwatch();
            var statsProvider = new TimeScalingSpeed(_ballConfig, _ballSpeedScale, stopwatch);
            
            gameUpdates.AddToUpdateList(stopwatch);

            return statsProvider;
        }
    }
}
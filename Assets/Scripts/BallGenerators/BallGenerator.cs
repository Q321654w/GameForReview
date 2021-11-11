using System;
using Balls;
using UpdateCollections;

namespace BallGenerators
{
    public class BallGenerator : IGameUpdate, IDisposable
    {
        public event Action<Ball> Spawned;
        
        private BallPlacer _ballPlacer;
        private BallProvider _ballProvider;
        
        private readonly float _spawnRate;
        private float _passedTime;

        public BallGenerator(BallPlacer ballPlacer, BallProvider ballProvider, float spawnRate)
        {
            _ballPlacer = ballPlacer;
            _ballProvider = ballProvider;
            _spawnRate = 1 / spawnRate;
        }

        public void GameUpdate(float deltaTime)
        {
            _passedTime += deltaTime;

            if (_passedTime < _spawnRate) return;

            _passedTime = 0;
            
            SpawnBall();
        }
        
        private void SpawnBall()
        {
            var ball = _ballProvider.GetBall();
            _ballPlacer.Place(ball);
            
            Spawned?.Invoke(ball);
        }

        public void Dispose()
        {
            Spawned = null;
            _ballProvider.Dispose();
        }
    }
}
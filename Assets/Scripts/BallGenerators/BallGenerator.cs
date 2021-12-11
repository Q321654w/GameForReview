using System;
using Balls;
using Common;

namespace BallGenerators
{
    public class BallGenerator : ICleanUp
    {
        public event Action<Ball> Spawned;

        private readonly IBallPlacer _ballPlacer;
        private readonly IBallProvider _ballProvider;

        private readonly RandomLoopTimer _timer;

        public BallGenerator(IBallPlacer ballPlacer, IBallProvider ballProvider, RandomLoopTimer timer)
        {
            _ballPlacer = ballPlacer;
            _ballProvider = ballProvider;

            _timer = timer;
            _timer.TimIsUp += SpawnBall;
            _timer.Resume();
        }

        private void SpawnBall()
        {
            var ball = _ballProvider.GetBall();
            _ballPlacer.PlaceBall(ball);

            Spawned?.Invoke(ball);
        }

        public void CleanUp()
        {
            _timer.TimIsUp -= SpawnBall;
            _timer.CleanUp();
        }
    }
}
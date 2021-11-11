using BallGenerators.Builder;
using Balls;
using Pools;

namespace BallGenerators
{
    public class BallProvider
    {
        private readonly BallBuilder _ballBuilder;
        private readonly Pool<Ball> _pool;

        public BallProvider(BallBuilder ballBuilder)
        {
            _ballBuilder = ballBuilder;
            _pool = new Pool<Ball>();
        }

        public Ball GetBall()
        {
            return _pool.HasInactiveObjects() ? GetInactiveBall() : CreateNewBall();
        }
        
        private Ball GetInactiveBall()
        {
            var ball = _pool.GetInactiveObject();

            _ballBuilder.InitializeBall(ball);

            return ball;
        }
        private Ball CreateNewBall()
        {
            var ball = _ballBuilder.BuildBall();

            _pool.Add(ball);

            return ball;
        }

        public void Dispose()
        {
            _ballBuilder.Dispose();
        }
    }
}
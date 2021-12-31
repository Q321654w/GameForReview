namespace Balls.Stats.Decorators
{
    public abstract class BallStatsDecorator : IBallStatsProvider
    {
        protected readonly IBallStatsProvider BallStatsProvider;

        protected BallStatsDecorator(IBallStatsProvider ballStatsProvider)
        {
            BallStatsProvider = ballStatsProvider;
        }

        public BallStats Stats => GetStatsInternal();

        protected abstract BallStats GetStatsInternal();
    }
}
namespace Balls.Stats.Decorators
{
    public abstract class BallStatsDecorator : IBallStatsProvider
    {
        protected IBallStatsProvider BallStatsProvider;
        
        public BallStatsDecorator(IBallStatsProvider ballStatsProvider)
        {
            BallStatsProvider = ballStatsProvider;
        }

        public BallStats Stats => GetStatsInternal();

        protected abstract BallStats GetStatsInternal();
    }
}
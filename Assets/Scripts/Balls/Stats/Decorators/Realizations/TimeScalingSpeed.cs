using Common;
using UnityEngine;

namespace Balls.Stats.Decorators.Realizations
{
    public class TimeScalingSpeed : BallStatsDecorator
    {
        private readonly AnimationCurve _curve;
        private readonly Stopwatch _stopwatch;

        public TimeScalingSpeed(IBallStatsProvider ballStatsProvider, AnimationCurve curve, Stopwatch stopwatch) : base(ballStatsProvider)
        {
            _curve = curve;
            _stopwatch = stopwatch;
        }

        protected override BallStats GetStatsInternal()
        {
            var speed = _curve.Evaluate(_stopwatch.PassedTimeInSeconds);
            return BallStatsProvider.Stats + new BallStats(speed);
        }
    }
}
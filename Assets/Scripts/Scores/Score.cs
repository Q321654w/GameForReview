using System;
using BallGenerators;

namespace Scores
{
    public class Score
    {
        public event Action<int> Changed;

        private BallGenerator _ballGenerator;
        public int CurrentScore { get; private set; }

        public Score(BallGenerator ballGenerator)
        {
            _ballGenerator = ballGenerator;
            _ballGenerator.Spawned += OnSpawned;
            CurrentScore = 0;
        }

        private void OnSpawned(IScoreProvider scoreProvider)
        {
            scoreProvider.Scored += AddPoints;
        }

        private void AddPoints(int points)
        {
            CurrentScore += points;
            Changed?.Invoke(CurrentScore);
        }
    }
}
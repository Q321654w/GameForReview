using System;
using BallGenerators;
using DefaultNamespace;

namespace Scores
{
    public class Score : ICleanUp
    {
        public event Action<int> Changed;

        private BallGenerator _ballGenerator;
        public int CurrentScore { get; private set; }

        public Score(BallGenerator ballGenerator, int currentScore)
        {
            _ballGenerator = ballGenerator;
            _ballGenerator.Spawned += OnSpawned;
            CurrentScore = currentScore;
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

        void ICleanUp.CleanUp()
        {
            _ballGenerator.Spawned -= OnSpawned;
        }
    }
}
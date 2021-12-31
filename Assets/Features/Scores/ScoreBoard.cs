using System;

namespace Scores
{
    [Serializable]
    public class ScoreBoard
    {
        public int BestScore { get; }

        public ScoreBoard(int bestScore)
        {
            BestScore = bestScore;
        }
    }
}
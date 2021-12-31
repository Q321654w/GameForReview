using System;

namespace Scores
{
    public interface IScoreProvider
    {
        public event Action<int> Scored;
    }
}
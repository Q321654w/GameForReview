using Scores;

namespace SaveSystem
{
    public interface ISaveSystem
    {
        void Save(Score score);
        ScoreBoard Load();
    }
}
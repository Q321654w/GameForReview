using System.Collections.Generic;
using Common;
using Games;
using SaveSystem;
using Scores;

namespace Operations
{
    public class EndGameOperation
    {
        private readonly List<ICleanUp> _cleanUps;
        private readonly Score _score;
        private readonly UI _ui;
        private readonly ISaveSystem _saveSystem;

        public EndGameOperation(List<ICleanUp> cleanUps, Score score, UI ui, ISaveSystem saveSystem)
        {
            _cleanUps = cleanUps;
            _score = score;
            _ui = ui;
            _saveSystem = saveSystem;
        }

        public void Execute()
        {
            foreach (var cleanUp in _cleanUps)
            {
                cleanUp.CleanUp();
            }
        
            var saveSystem = new BinarySaveSystem();
            SaveResults(saveSystem);
        
            var scoreBoard = saveSystem.Load();
            DisplayResults(scoreBoard);
        }

        private void DisplayResults(ScoreBoard scoreBoard)
        {
            _ui.ShowEndView(_score, scoreBoard);
        }

        private void SaveResults(ISaveSystem saveSystem)
        {
            var scoreBoard = saveSystem.Load();
            if (scoreBoard == null || scoreBoard.BestScore <= _score.CurrentScore)
            {
                saveSystem.Save(_score);
            }
        }
    }
}
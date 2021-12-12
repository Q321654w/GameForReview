using Common;
using Games;
using SaveSystem;
using Scores;

namespace Operations
{
    public class EndGameOperation
    {
        private readonly Game _game;
        private readonly Score _score;
        private readonly UI _ui;

        public EndGameOperation(Game game, Score score, UI ui)
        {
            _game = game;
            _score = score;
            _ui = ui;
        
            EndGame();
        }

        private void EndGame()
        {
            _game.CleanUp();
        
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
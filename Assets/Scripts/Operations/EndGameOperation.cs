using DefaultNamespace;
using SaveSystem;
using Scores;

public class EndGameOperation
{
    private readonly ICleanUp _cleanUp;
    private readonly Score _score;
    private readonly UI _ui;

    public EndGameOperation(ICleanUp cleanUp, Score score, UI ui)
    {
        _cleanUp = cleanUp;
        _score = score;
        _ui = ui;
        
        EndGame();
    }

    private void EndGame()
    {
        _cleanUp.CleanUp();
        
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
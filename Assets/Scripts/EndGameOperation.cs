﻿using SaveSystem;
using Scores;
using UpdateCollections;

public class EndGameOperation
{
    private readonly UpdateCollection _updateCollection;
    private readonly Score _score;
    private readonly UI _ui;

    public EndGameOperation(UpdateCollection updateCollection, Score score, UI ui)
    {
        _updateCollection = updateCollection;
        _score = score;
        _ui = ui;
        
        EndGame();
    }

    private void EndGame()
    {
        _updateCollection.StopUpdate();
        
        var saveSystem = new BinarySaveSystem();
        var scoreBoard = saveSystem.Load();
        
        SaveResults(saveSystem,scoreBoard);
        DisplayResults(scoreBoard);
    }

    private void DisplayResults(ScoreBoard scoreBoard)
    {
        _ui.ShowEndView(_score, scoreBoard);
    }

    private void SaveResults(ISaveSystem saveSystem,ScoreBoard scoreBoard)
    {
        if (scoreBoard == null || scoreBoard.BestScore <= _score.CurrentScore)
        {
            saveSystem.Save(_score);
        }
    }
}
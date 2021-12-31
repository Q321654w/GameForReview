using Common;
using Games;
using Operations;
using Players;
using Scores;
using UnityEngine;

public class UI : ICleanUp
{
    private readonly Camera _camera;
    private readonly Canvas _canvas;
    private readonly HealthView _healthView;
    private readonly EndGameView _endGameView;
    private readonly ScoreView _scoreView;

    public UI(Canvas canvas, HealthView healthView, EndGameView endGameView, ScoreView scoreView, Camera camera)
    {
        _canvas = canvas;
        _healthView = healthView;
        _endGameView = endGameView;
        _scoreView = scoreView;
        _camera = camera;

        _canvas.worldCamera = camera;
    }

    public void ShowEndView(Score score, ScoreBoard scoreBoard)
    {
        var newGameOperation = new NewGameOperation();
        _endGameView.Initialize(score, scoreBoard, newGameOperation);
        _endGameView.gameObject.SetActive(true);
    }

    void ICleanUp.CleanUp()
    {
        _scoreView.CleanUp();
        _healthView.CleanUp();
        _scoreView.CleanUp();
    }
}
using Common;
using DefaultNamespace;
using Games;
using Players;
using Scores;
using UnityEngine;

public class UI : ICleanUp
{
    private readonly Camera _camera;
    private readonly Canvas _canvas;
    private readonly PlayerView _playerView;
    private readonly EndGameView _endGameView;
    private readonly ScoreView _scoreView;

    public UI(Canvas canvas, PlayerView playerView, EndGameView endGameView, ScoreView scoreView, Camera camera)
    {
        _canvas = canvas;
        _playerView = playerView;
        _endGameView = endGameView;
        _scoreView = scoreView;
        _camera = camera;
        
        _canvas.worldCamera = camera;
    }

    public void ShowEndView(Score score, ScoreBoard scoreBoard)
    {
        _endGameView.Initialize(score, scoreBoard);
        _endGameView.gameObject.SetActive(true);
    }

    void ICleanUp.CleanUp()
    {
        _scoreView.CleanUp();
        _playerView.CleanUp();
        _scoreView.CleanUp();
    }

}
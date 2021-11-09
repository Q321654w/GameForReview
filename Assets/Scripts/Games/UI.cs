using Games;
using Scores;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private ScoreView _scoreViewPrefab;
    [SerializeField] private EndGameView _endGameViewPrefab;
    [SerializeField] private Canvas _canvasPrefab;

    private Canvas _canvas;

    public void ShowScore(Score score)
    {
        _canvas = Instantiate(_canvasPrefab);
        var scoreView = Instantiate(_scoreViewPrefab, _canvas.transform);
        scoreView.Initialize(score);
    }

    public void ShowEndView(Score score, ScoreBoard scoreBoard)
    {
        var endGameView = Instantiate(_endGameViewPrefab, _canvas.transform);
        endGameView.Initialize(score, scoreBoard);
    }
}
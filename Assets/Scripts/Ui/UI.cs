using Games;
using IDamageables;
using Scores;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private ScoreView _scoreViewPrefab;
    [SerializeField] private EndGameView _endGameViewPrefab;
    [SerializeField] private Canvas _canvasPrefab;
    [SerializeField] private Slider _sliderPrefab;

    private Canvas _canvas;
    private Slider _slider;
    private EndGameView _endGameView;
    private ScoreView _scoreView;

    public void Initialize(Camera camera)
    {
        _canvas = Instantiate(_canvasPrefab);
        _canvas.worldCamera = camera;
    }
    
    public void ShowHealth(Health health,int maxHitPoints)
    {
        _slider = Instantiate(_sliderPrefab, _canvas.transform);
        _slider.maxValue = maxHitPoints;
        _slider.value = maxHitPoints;
        health.Changed += OnChanged;
    }
    private void OnChanged(int hitPoints)
    {
        _slider.value = hitPoints;
    }
    
    public void ShowScore(Score score)
    {
        _scoreView = Instantiate(_scoreViewPrefab, _canvas.transform);
        _scoreView.Initialize(score);
    }

    public void ShowEndView(Score score, ScoreBoard scoreBoard)
    {
        _endGameView = Instantiate(_endGameViewPrefab, _canvas.transform);
        _endGameView.Initialize(score, scoreBoard);
    }
}
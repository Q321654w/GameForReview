using Games;
using IDamageables;
using Players;
using Scores;
using UnityEngine;

namespace Installers
{
    public class UiInstaller : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Canvas _canvasPrefab;
        [SerializeField] private ScoreView _scoreViewPrefab;
        [SerializeField] private EndGameView _endGameViewPrefab;
        [SerializeField] private PlayerView _playerViewPrefab;

        public UI Install(Score score, Health health, int maxHitPoint)
        {
            return CreateUi(score, health, maxHitPoint);
        }

        private UI CreateUi(Score score, Health playerHealth, int maxHitPoint)
        {
            var canvas = Instantiate(_canvasPrefab);

            var endGameView = Instantiate(_endGameViewPrefab, canvas.transform);
            endGameView.gameObject.SetActive(false);

            var playerView = Instantiate(_playerViewPrefab, canvas.transform);
            playerView.Initialize(playerHealth, maxHitPoint);

            var scoreView = Instantiate(_scoreViewPrefab, canvas.transform);
            scoreView.Initialize(score);

            var ui = new UI(canvas, playerView, endGameView, scoreView, _camera);

            return ui;
        }
    }
}
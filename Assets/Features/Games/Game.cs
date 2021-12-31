using BallGenerators;
using Balls;
using Common;
using GameAreas;
using GameUpdate;
using Operations;
using Players;
using Scores;

namespace Games
{
    public class Game : ICleanUp
    {
        private readonly Player _player;
        private readonly Score _score;
        private readonly GameArea _gameArea;
        private readonly BallGenerator _ballGenerator;
        private readonly PlayerView _playerView;
        private readonly UI _ui;
        private readonly GameUpdates _gameUpdates;
        
        private readonly EndGameOperation _endGameOperation;

        public Game(Player player, Score score, GameArea gameArea, GameUpdates gameUpdates, UI ui, BallGenerator ballGenerator, PlayerView playerView, EndGameOperation endGameOperation)
        {
            _score = score;
            _gameArea = gameArea;
            _gameUpdates = gameUpdates;
            _ui = ui;
            _ballGenerator = ballGenerator;
            _playerView = playerView;
            _endGameOperation = endGameOperation;
            _player = player;
        }

        public void Start()
        {
            _ballGenerator.Spawned += OnSpawned;
            _player.Died += EndGame;
            
            _gameUpdates.ResumeUpdate();
        }

        private void OnSpawned(Ball ball)
        {
            _gameUpdates.AddToUpdateList(ball);
        }

        private void EndGame()
        {
            _gameUpdates.StopUpdate();
            _endGameOperation.Execute();
        }

        public void CleanUp()
        {
            _ballGenerator.Spawned -= OnSpawned;
        }
    }
}
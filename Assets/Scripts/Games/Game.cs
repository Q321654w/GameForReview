using System.Collections.Generic;
using BallGenerators;
using Balls;
using DefaultNamespace;
using GameAreaes;
using Players;
using Scores;
using UpdateCollections;

namespace Games
{
    public class Game : ICleanUp
    {
        private readonly Player _player;
        private readonly Score _score;
        private readonly GameArea _gameArea;
        private readonly BallGenerator _ballGenerator;
        private readonly PlayerDamager _playerDamager;
        private readonly UI _ui;
        private readonly GameUpdates _gameUpdates;
        
        private List<ICleanUp> _cleanups;

        public Game(Player player, Score score, GameArea gameArea, GameUpdates gameUpdates, UI ui, BallGenerator ballGenerator, PlayerDamager playerDamager, List<ICleanUp> cleanups)
        {
            _score = score;
            _gameArea = gameArea;
            _gameUpdates = gameUpdates;
            _ui = ui;
            _ballGenerator = ballGenerator;
            _playerDamager = playerDamager;
            _cleanups = cleanups;
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
            new EndGameOperation(this, _score, _ui);
        }

        public void CleanUp()
        {
            _ballGenerator.Spawned -= OnSpawned;
            
            foreach (var cleanup in _cleanups)
            {
                cleanup.CleanUp();
            }
        }
    }
}
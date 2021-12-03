using System.Collections.Generic;
using BallGenerators;
using BallGenerators.Builder;
using Balls;
using Balls.Stats.Decorators;
using Balls.Stats.Decorators.Realizations;
using DefaultNamespace;
using GameAreaes;
using GameAreaes.Borders;
using Players;
using Scores;
using UnityEngine;
using UpdateCollections;

namespace Games
{
    [RequireComponent(typeof(UI))]
    [RequireComponent(typeof(UpdateCollection))]
    public class Game : MonoBehaviour, ICleanUp
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Border _borderPrefab;
        [SerializeField] private GameSettings _gameSettings;

        private Score _score;
        private GameArea _gameArea;
        private UpdateCollection _updateCollection;
        private UI _ui;
        private List<ICleanUp> _cleanups;

        private void Awake()
        {
            _ui = GetComponent<UI>();
            _updateCollection = GetComponent<UpdateCollection>();
            _cleanups = new List<ICleanUp>();
        }

        private void Start()
        {
            CreateGameArea();
            
            var ballGenerator = CreateBallGenerator();
            var player = CreatePlayer();
            
            CreateScore(ballGenerator);

            _cleanups.Add(player);
            _cleanups.Add(_updateCollection);
            _cleanups.Add(ballGenerator);

            _ui.ShowScore(_score);
        }

        private void CreateGameArea()
        {
            var border = Instantiate(_borderPrefab);
            _gameArea = new GameArea(_camera, _gameSettings.BorderOffset, border);
        }

        private Player CreatePlayer()
        {
            var playerBuilder = new PlayerBuilder();

            var damage = _gameSettings.PlayerDamage;
            var hitPoints = _gameSettings.PlayerHitPoints;
            var player = playerBuilder.BuildPlayer(EndGame, _gameArea, hitPoints, damage);

            _updateCollection.AddToUpdateList(player);
            
            return player;
        }

        private BallGenerator CreateBallGenerator()
        {
            var ballProvider = CreateBallProvider();

            var ballPlacer = new BallPlacer(_gameArea);
            var ballGenerator = new BallGenerator(ballPlacer, ballProvider, _gameSettings.SpawnRate);
            
            ballGenerator.Spawned += OnSpawned;
            _updateCollection.AddToUpdateList(ballGenerator);

            return ballGenerator;
        }

        private void OnSpawned(Ball ball)
        {
            _updateCollection.AddToUpdateList(ball);
        }

        private BallProvider CreateBallProvider()
        {
            var statsProvider = CreateStatsProvider();
            var dieEffect = _gameSettings.BallConfig.DieEffect;
            
            var ballBuilder = new BallBuilder(statsProvider, _gameSettings.BallConfig.Prefab, dieEffect);
            var ballProvider = new BallProvider(ballBuilder);
            
            return ballProvider;
        }

        private IBallStatsProvider CreateStatsProvider()
        {
            var stopwatch = new Stopwatch();
            var statsProvider = new TimeScalingSpeed(_gameSettings.BallConfig, _gameSettings.BallSpeedScale, stopwatch);

            _updateCollection.AddToUpdateList(stopwatch);
            _cleanups.Add(stopwatch);

            return statsProvider;
        }

        private void CreateScore(BallGenerator ballGenerator)
        {
            _score = new Score(ballGenerator);
        }

        private void EndGame()
        {
            new EndGameOperation(this, _score, _ui);
        }

        public void CleanUp()
        {
            foreach (var cleanup in _cleanups)
            {
                cleanup.CleanUp();
            }
        }
    }
}
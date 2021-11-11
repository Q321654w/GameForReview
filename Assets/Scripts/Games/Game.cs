using BallGenerators;
using BallGenerators.Builder;
using Balls.Stats.Decorators;
using Balls.Stats.Decorators.Realizations;
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
    public class Game : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Border _borderPrefab;
        [SerializeField] private GameSettings _gameSettings;

        private Score _score;
        private GameArea _gameArea;
        private UpdateCollection _updateCollection;
        private BallGenerator _ballGenerator;
        private UI _ui;

        private void Awake()
        {
            _ui = GetComponent<UI>();
            _updateCollection = GetComponent<UpdateCollection>();
            _updateCollection.StartUpdate();
        }

        private void Start()
        {
            CreateGameArea();
            _ballGenerator = CreateBallGenerator();
            CreatePlayer();
            CreateScore(_ballGenerator);
            
            _ui.ShowScore(_score);
        }
        private void CreateGameArea()
        {
            var border = Instantiate(_borderPrefab);
            _gameArea = new GameArea(_camera, _gameSettings.BorderOffset, border);
        }
        private void CreatePlayer()
        {
            var playerBuilder = new PlayerBuilder();

            var damage = _gameSettings.PlayerDamage;
            var hitPoints = _gameSettings.PlayerHitPoints;
            var player = playerBuilder.BuildPlayer(_gameArea, hitPoints, damage);

            _updateCollection.AddToUpdateQueue(player);
            player.Health.Died += EndGame;
        }
        private BallGenerator CreateBallGenerator()
        {
            var ballProvider = CreateBallProvider();

            var ballPlacer = new BallPlacer(_gameArea);
            var ballGenerator = new BallGenerator(ballPlacer, ballProvider, _gameSettings.SpawnRate);
            _updateCollection.AddToUpdateQueue(ballGenerator);

            return ballGenerator;
        }
        private BallProvider CreateBallProvider()
        {
            var statsProvider = CreateStatsProvider();
            var ballBuilder = new BallBuilder(statsProvider, _gameSettings.BallConfig.Prefab);
            ballBuilder.Builded += ball=> _updateCollection.AddToUpdateQueue(ball);
            var ballProvider = new BallProvider(ballBuilder);
            return ballProvider;
        }
        private IBallStatsProvider CreateStatsProvider()
        {
            var stopwatch = new Stopwatch();
            var statsProvider = new TimeScalingSpeed(_gameSettings.BallConfig, _gameSettings.BallSpeedScale, stopwatch);

            _updateCollection.AddToUpdateQueue(stopwatch);
            return statsProvider;
        }
        private void CreateScore(BallGenerator ballGenerator)
        {
            _score = new Score(ballGenerator);
        }

        private void EndGame()
        {
            _ballGenerator.Dispose();
            new EndGameOperation(_updateCollection, _score, _ui);
        }
    }
}
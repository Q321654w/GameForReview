using System.Collections.Generic;
using BallGenerators;
using Common;
using GameAreas;
using GameAreas.Borders;
using Games;
using IDamageables;
using Players;
using Scores;
using UnityEngine;

namespace Installers
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [SerializeField] private int _playerHitPoints;
        [SerializeField] private int _playerDamage;

        [SerializeField] private Border _borderPrefab;
        [SerializeField] private Vector2 _borderOffset;

        [SerializeField] private GameUpdates.GameUpdates _gameUpdates;

        [SerializeField] private UiInstaller _uiInstaller;
        [SerializeField] private BallGeneratorInstaller _ballGeneratorInstaller;

        private void Start()
        {
            Install();
        }

        private void Install()
        {
            var gameArea = CreateGameArea();
            var border = CreateBorder(gameArea);

            var ballGenerator = _ballGeneratorInstaller.Install(gameArea, _gameUpdates);
            var playerHealth = CreatePlayerHealth();
            var player = CreatePlayer(playerHealth, gameArea);
            var playerDamager = CreatePlayerDamager(player, border);

            var score = CreateScore(ballGenerator);

            var ui = _uiInstaller.Install(score, playerHealth, _playerHitPoints);

            var cleanUps = new List<ICleanUp>()
            {
                player, playerDamager, ui, _gameUpdates, ballGenerator, score
            };

            var game = new Game(player, score, gameArea, _gameUpdates, ui, ballGenerator, playerDamager, cleanUps);
            game.Start();
        }

        private GameArea CreateGameArea()
        {
            var gameArea = new GameArea(_camera);
            return gameArea;
        }

        private Border CreateBorder(GameArea gameArea)
        {
            var border = Instantiate(_borderPrefab);
            var size = new Vector2(gameArea.Size.x, 1);
            border.Initialize(size);

            gameArea.PlaceObjectAtBottomWithOffset(_borderOffset, border.transform);
            return border;
        }

        private Health CreatePlayerHealth()
        {
            var hitPoints = _playerHitPoints;
            return new Health(hitPoints);
        }

        private Player CreatePlayer(Health playerHealth, GameArea gameArea)
        {
            var damage = _playerDamage;
            var playerInput = new PlayerInput();

            var player = new Player(playerInput, gameArea, damage, playerHealth);
            
            _gameUpdates.AddToUpdateList(player);
            return player;
        }

        private PlayerDamager CreatePlayerDamager(Player player, Border border)
        {
            return new PlayerDamager(player, border);
        }

        private Score CreateScore(BallGenerator ballGenerator)
        {
            return new Score(ballGenerator, 0);
        }
    }
}
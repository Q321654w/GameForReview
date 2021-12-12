using System.Collections.Generic;
using BallGenerators;
using Common;
using GameAreas;
using Games;
using GameUpdate;
using IDamageables;
using Players;
using Scores;
using UnityEngine;

namespace Installers
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private Camera _cameraPrefab;

        [SerializeField] private int _playerHitPoints;
        [SerializeField] private int _playerDamage;

        [SerializeField] private PlayerView _playerViewPrefab;
        [SerializeField] private Vector2 _playerOffset;

        [SerializeField] private GameUpdates _gameUpdates;

        [SerializeField] private UiInstaller _uiInstaller;
        [SerializeField] private BallGeneratorInstaller _ballGeneratorInstaller;

        private void Start()
        {
            Install();
        }

        private void Install()
        {
            var cameraInstance = Instantiate(_cameraPrefab);
            var gameArea = CreateGameArea(cameraInstance);

            var ballGenerator = _ballGeneratorInstaller.Install(gameArea, _gameUpdates);
            var playerHealth = CreatePlayerHealth();
            var player = CreatePlayer(playerHealth, gameArea);
            var playerDamager = CreatePlayerView(player, gameArea);

            var score = CreateScore(ballGenerator);

            var ui = _uiInstaller.Install(score, playerHealth, _playerHitPoints, cameraInstance);

            var cleanUps = new List<ICleanUp>()
            {
                player, ui, _gameUpdates, ballGenerator, score
            };

            var game = new Game(player, score, gameArea, _gameUpdates, ui, ballGenerator, playerDamager, cleanUps);
            game.Start();
            
            DestroyInstallers();
        }

        private void DestroyInstallers()
        {
            Destroy(gameObject);
        }

        private GameArea CreateGameArea(Camera cameraInstance)
        {
            var gameArea = new GameArea(cameraInstance);
            return gameArea;
        }

        private PlayerView CreatePlayerView(Player player, GameArea gameArea)
        {
            var playerView = Instantiate(_playerViewPrefab);
            var size = new Vector2(gameArea.Size.x, 1);
            playerView.Initialize(player, size);

            gameArea.PlaceObjectAtBottomBorderWithOffset(_playerOffset, playerView.transform);
            return playerView;
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

        private Score CreateScore(BallGenerator ballGenerator)
        {
            return new Score(ballGenerator, 0);
        }
    }
}
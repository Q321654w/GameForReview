using Balls;
using GameAreas;
using UnityEngine;

namespace BallGenerators
{
    public class BallPlacer : IBallPlacer
    {
        private readonly GameArea _gameArea;

        public BallPlacer(GameArea gameArea)
        {
            _gameArea = gameArea;
        }

        public void PlaceBall(Ball ball)
        {
            var transform = ball.transform;
            
            var ballSizeOffset = transform.localScale.x / 2;
            var xOffset = _gameArea.GetRandomXInAreaWithOffsetAtBorders(ballSizeOffset);
            var yOffset = ball.transform.localScale.y / 2;
            var offset = new Vector2(xOffset, yOffset);

            _gameArea.PlaceObjectAtTopBorderWithOffset(offset, transform);
        }
    }
}
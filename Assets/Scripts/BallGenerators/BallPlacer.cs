﻿using Balls;
using GameAreaes;
using UnityEngine;

namespace BallGenerators
{
    public class BallPlacer
    {
        private readonly GameArea _gameArea;

        public BallPlacer(GameArea gameArea)
        {
            _gameArea = gameArea;
        }
        
        public void Place(Ball ball)
        {
            var transform = ball.transform;

            var xOffset = _gameArea.GetRandomXPosition();
            var yOffset = ball.transform.localScale.y / 2;
            var offset = new Vector2(xOffset, yOffset);

            _gameArea.PlaceObjectAtTopWithOffset(offset, transform);
            ball.Initialize(Vector2.down);
        }
    }
}
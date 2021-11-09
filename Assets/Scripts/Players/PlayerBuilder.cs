﻿using GameAreaes;
using IDamageables;

namespace Players
{
    public class PlayerBuilder
    {
        public Player BuildPlayer(GameArea gameArea,int hitPoints,int damage)
        {
            var playerInput = new PlayerInput();
            var playerHealth = new Health(hitPoints);
            var player = new Player(playerInput, gameArea, damage, playerHealth);
            var playerDamager = new PlayerDamager(player, gameArea.BottomBorder);
            return player;
        }

    }
}
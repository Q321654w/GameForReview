using System;

namespace GameUpdate
{
    public interface IGameUpdate
    {
        event Action<IGameUpdate> UpdateRemoveRequested;
        void GameUpdate(float deltaTime);
    }
}
using System;

namespace GameUpdates
{
    public interface IGameUpdate
    {
        event Action<IGameUpdate> UpdateRemoveRequested;
        void GameUpdate(float deltaTime);
    }
}
using System;

namespace UpdateCollections
{
    public interface IGameUpdate
    {
        event Action<IGameUpdate> UpdateRemoveRequested;
        void GameUpdate(float deltaTime);
    }
}
using System;
using UpdateCollections;

public class Stopwatch : IGameUpdate
{
    public event Action<IGameUpdate> UpdateRemoveRequested;
    
    public float PassedTimeInSeconds { get; private set; }
    
    public void GameUpdate(float deltaTime)
    {
        PassedTimeInSeconds += deltaTime;
    }
}
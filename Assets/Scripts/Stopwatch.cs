using System;
using DefaultNamespace;
using UpdateCollections;

public class Stopwatch : IGameUpdate, ICleanUp
{
    public event Action<IGameUpdate> UpdateRemoveRequested;
    
    public float PassedTimeInSeconds { get; private set; }
    
    public void GameUpdate(float deltaTime)
    {
        PassedTimeInSeconds += deltaTime;
    }

    public void CleanUp()
    {
        UpdateRemoveRequested?.Invoke(this);
    }
}
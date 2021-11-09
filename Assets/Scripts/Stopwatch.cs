using UpdateCollections;

public class Stopwatch : IGameUpdate
{
    public float PassedTimeInSeconds { get; private set; }

    public void GameUpdate(float deltaTime)
    {
        PassedTimeInSeconds += deltaTime;
    }
}
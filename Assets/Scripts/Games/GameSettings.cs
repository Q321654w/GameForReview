using BallGenerators.Builder.Configs;
using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private BallConfig _ballBallConfig;
    [SerializeField] private AnimationCurve _ballSpeedScale;
    [SerializeField] private float _borderOffset;
    [SerializeField] private int _spawnRate;
    [SerializeField] private int _playerHitPoints;
    [SerializeField] private int _playerDamage;

    public BallConfig BallConfig => _ballBallConfig;
    public AnimationCurve BallSpeedScale => _ballSpeedScale;
    public float BorderOffset => _borderOffset;
    public int SpawnRate => _spawnRate;
    public int PlayerHitPoints => _playerHitPoints;
    public int PlayerDamage => _playerDamage;
}
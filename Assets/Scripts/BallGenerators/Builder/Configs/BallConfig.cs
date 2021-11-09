using Balls;
using Balls.Stats;
using Balls.Stats.Decorators;
using UnityEngine;

namespace BallGenerators.Builder.Configs
{
    [CreateAssetMenu(menuName = "BallConfig")]
    public class BallConfig : ScriptableObject, IBallStatsProvider
    {
        [SerializeField] private BallStats _ballStats;
        [SerializeField] private Ball _prefab;
        
        public BallStats Stats => _ballStats;
        public Ball Prefab => _prefab;
    }
}
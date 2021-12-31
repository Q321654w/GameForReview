using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(menuName = "Effect")]
    public class Effect : ScriptableObject
    {
        [SerializeField] private ParticleSystem _particleSystem;
        
        public void Play(Transform point, Color color)
        {
            var particles = Object.Instantiate(_particleSystem, point.position, Quaternion.identity);

            var particlesMain = particles.main;
            particlesMain.startColor = color;
            particles.Play();
        }
    }
}
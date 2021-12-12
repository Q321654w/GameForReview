using Common;
using IDamageables;
using UnityEngine;
using UnityEngine.UI;

namespace Players
{
    public class HealthView : MonoBehaviour, ICleanUp
    {
        [SerializeField] private Slider _slider;
        private Health _health;

        public void Initialize(Health health, int maxValue)
        {
            _slider.maxValue = maxValue;
            _slider.value = maxValue;
            
            _health = health;
            _health.Changed += OnChanged;
        }

        private void OnChanged(int value)
        {
            _slider.value = value;
        }

        public void CleanUp()
        {
            _health.Changed -= OnChanged;
        }
    }
}
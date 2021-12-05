using System;

namespace IDamageables
{
    public class Health
    {
        public event Action Died;
        public event Action<int> Changed;

        private int _hitPoints;

        public Health(int startHitPoints)
        {
            _hitPoints = startHitPoints;
        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            Changed?.Invoke(_hitPoints);
            
            if (IsDead())
                Died?.Invoke();
        }

        private bool IsDead()
        {
            return _hitPoints <= 0;
        }
    }
}
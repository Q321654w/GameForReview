using UnityEngine;

namespace Movements
{
    public class Movement
    {
        private Transform _transform;
        private float _speed;

        public Movement(Transform transform, float speed)
        {
            _transform = transform;
            _speed = speed;
        }

        public void Move(Vector2 direction,float deltaTime)
        {
            var translation = direction * (_speed * deltaTime);
            _transform.Translate(translation);
        }
    }
}
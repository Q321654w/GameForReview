using Movements.DirectionProviders;
using UnityEngine;

namespace Movements
{
    public class Movement
    {
        private Transform _transform;
        private IDirectionProvider _directionProvider;
        private float _speed;
        
        public Movement(Transform transform, float speed, IDirectionProvider directionProvider)
        {
            _transform = transform;
            _speed = speed;
            _directionProvider = directionProvider;
        }

        public void Move(float deltaTime)
        {
            var direction = _directionProvider.GetDirection();
            var translation = direction * (_speed * deltaTime);
            _transform.Translate(translation);
        }
    }
}
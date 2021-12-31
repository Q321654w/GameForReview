using UnityEngine;

namespace Movements.DirectionProviders
{
    public class ConstantDirectionProvider : IDirectionProvider
    {
        private static readonly Vector2 Direction = Vector2.down;
        
        public Vector2 GetDirection()
        {
            return Direction;
        }
    }
}
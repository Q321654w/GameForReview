using UnityEngine;

namespace Movements.DirectionProviders
{
    public class BallDirectionProvider : IDirectionProvider
    {
        public Vector2 GetDirection()
        {
            return Vector2.down;
        }
    }
}
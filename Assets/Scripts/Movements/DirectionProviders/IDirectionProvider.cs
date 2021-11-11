using UnityEngine;

namespace Movements.DirectionProviders
{
    public interface IDirectionProvider
    {
        Vector2 GetDirection();

    }
}
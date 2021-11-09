using System;
using UnityEngine;

namespace Players
{
    public class PlayerInput
    {
        public event Action<Vector2> Clicked;

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var mousePosition = Input.mousePosition;
                Clicked?.Invoke(mousePosition);
            }
        }
    }
}
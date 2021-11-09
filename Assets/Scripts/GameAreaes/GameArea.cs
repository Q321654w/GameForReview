using GameAreaes.Borders;
using UnityEngine;

namespace GameAreaes
{
    public class GameArea
    {
        public Border BottomBorder { get; }

        private readonly Camera _camera;
        private readonly Vector2 _halfSize;
        
        private Vector2 Size => _halfSize * 2;

        public GameArea(Camera camera, float borderOffset, Border bottomBorder)
        {
            _camera = camera;
            _halfSize = new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);
            
            BottomBorder = bottomBorder;
            BottomBorder.Initialize(new Vector2(Size.x, 1));
            PlaceObjectAtBottomWithOffset(new Vector2(0, -borderOffset), BottomBorder.transform);
        }

        public float GetRandomXPosition()
        {
            var xPosition = _camera.transform.position.x;

            return Random.Range(xPosition - _halfSize.x, xPosition + _halfSize.x);
        }

        private void PlaceObjectAtBottomWithOffset(Vector2 offset, Transform transform)
        {
            var x = _camera.transform.position.x;
            var position = new Vector2(x, GetBottomY()) + offset;

            PlaceObjectAt(position, transform);
        }
        private float GetBottomY()
        {
            return _camera.transform.position.y - _halfSize.y;
        }
        public void PlaceObjectAtTopWithOffset(Vector2 offset, Transform transform)
        {
            var x = _camera.transform.position.x;
            var position = new Vector2(x, GetTopY()) + offset;

            PlaceObjectAt(position, transform);
        }
        private float GetTopY()
        {
            return _camera.transform.position.y + _halfSize.y;
        }
        private void PlaceObjectAt(Vector2 position, Transform transform)
        {
            transform.position = position;
        }

        public RaycastHit2D GetObjectAt(Vector2 position)
        {
            var areaPoint = ScreenPointToAreaPoint(position);
            var hit = Physics2D.Raycast(areaPoint, Vector3.forward, 100);

            return hit;
        }
        private Vector3 ScreenPointToAreaPoint(Vector2 position)
        {
            return _camera.ScreenToWorldPoint(position);
        }
    }
}
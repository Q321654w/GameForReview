using UnityEngine;

namespace GameAreas
{
    public class GameArea
    {
        public Vector2 Size { get; }

        private readonly Camera _camera;
        private readonly Vector2 _halfSize;
        private const float RAYCAST_DISTANCE = 100f;

        public GameArea(Camera camera)
        {
            _camera = camera;
            _halfSize = new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);
            Size = _halfSize * 2;
        }

        public float GetRandomXPosition()
        {
            var xPosition = _camera.transform.position.x;

            return Random.Range(xPosition - _halfSize.x, xPosition + _halfSize.x);
        }

        public void PlaceObjectAtBottomWithOffset(Vector2 offset, Transform transform)
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
            var hit = Physics2D.Raycast(areaPoint, Vector3.forward, RAYCAST_DISTANCE);

            return hit;
        }
        
        private Vector3 ScreenPointToAreaPoint(Vector2 position)
        {
            return _camera.ScreenToWorldPoint(position);
        }
    }
}
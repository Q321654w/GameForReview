using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ranges
{
    [Serializable]
    public struct FloatRange
    {
        [SerializeField, Range(0, 100)] private float _min;
        [SerializeField, Range(0, 100)] private float _max;

        public float Min => _min;
        public float Max => _max;

        public FloatRange(float min, float max)
        {
            _min = min;
            _max = max;
        }

        public float GetRandomValue()
        {
            return Random.Range(_min, _max);
        }

        public static FloatRange operator +(FloatRange range1, FloatRange range2)
        {
            var max = range1.Max + range2.Max;
            var min = range1.Min + range2.Min;
            return new FloatRange(min, max);
        }
    }
}
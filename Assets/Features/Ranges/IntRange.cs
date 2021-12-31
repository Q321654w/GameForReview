using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ranges
{
    [Serializable]
    public struct IntRange
    {
        [SerializeField, Range(0, 100)] private int _min;
        [SerializeField, Range(0, 100)] private int _max;

        public int Min => _min;
        public int Max => _max;

        public IntRange(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public int GetRandomValue()
        {
            return Random.Range(_min, _max + 1);
        }

        public static IntRange operator +(IntRange range1, IntRange range2)
        {
            var max = range1.Max + range2.Max;
            var min = range1.Min + range2.Min;
            return new IntRange(min, max);
        }
    }
}
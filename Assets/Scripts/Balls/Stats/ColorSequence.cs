using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Balls.Stats
{
    [Serializable]
    public struct ColorSequence
    {
        [SerializeField] private Color[] _colors;

        public ColorSequence(Color[] colorsArray)
        {
            _colors = colorsArray;
        }

        public Color GetRandomColor()
        {
            var length = _colors.Length;
            var index = Random.Range(0, length);
            return _colors[index];
        }

        public static ColorSequence operator +(ColorSequence sequence1, ColorSequence sequence2)
        {
            Color[] colors;
            int firstLength;
            int secondLength;

            if (sequence1._colors == null)
            {
                secondLength = sequence2._colors.Length - 1;
                colors = new Color[secondLength];

                for (var i = 0; i < secondLength; i++)
                {
                    colors[i] = sequence2._colors[i];
                }

                return new ColorSequence(colors);
            }

            if (sequence2._colors == null)
            {
                firstLength = sequence1._colors.Length - 1;
                colors = new Color[firstLength];

                for (var i = 0; i < firstLength; i++)
                {
                    colors[i] = sequence1._colors[i];
                }

                return new ColorSequence(colors);
            }

            firstLength = sequence1._colors.Length - 1;
            secondLength = sequence2._colors.Length - 1;
            var length = firstLength + secondLength;

            colors = new Color[length];

            sequence1._colors.CopyTo(colors, 0);
            sequence2._colors.CopyTo(colors, firstLength);

            return new ColorSequence(colors);
        }
    }
}
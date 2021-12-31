using Common;
using UnityEngine;
using UnityEngine.UI;

namespace Scores
{
    public class ScoreView : MonoBehaviour, ICleanUp
    {
        [SerializeField] private Text _text;
        private Score _score;

        public void Initialize(Score score)
        {
            _score = score;
            _score.Changed += OnChanged;
        }

        private void OnChanged(int score)
        {
            _text.text = "" + score;
        }

        public void CleanUp()
        {
            _score.Changed -= OnChanged;
        }
    }
}
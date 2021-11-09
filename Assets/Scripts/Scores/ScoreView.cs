using UnityEngine;
using UnityEngine.UI;

namespace Scores
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public void Initialize(Score score)
        {
            score.Changed += OnChanged;
        }

        private void OnChanged(int score)
        {
            _text.text = "" + score;
        }
    }
}
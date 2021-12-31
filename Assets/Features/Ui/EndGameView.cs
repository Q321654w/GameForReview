using Common;
using Operations;
using Scores;
using UnityEngine;
using UnityEngine.UI;

namespace Games
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField] private Text _bestScore;
        [SerializeField] private Text _currentScore;
        [SerializeField] private CustomButton _restartButton;

        private NewGameOperation _newGameOperation;

        public void Initialize(Score score, ScoreBoard scoreBoard,NewGameOperation newGameOperation)
        {
            _newGameOperation = newGameOperation;
            _bestScore.text = "Best Score : " + scoreBoard.BestScore;
            _currentScore.text = "Current Score : " + score.CurrentScore;
            _restartButton.Pressed += RestartGame;
        }

        private void RestartGame()
        {
            _newGameOperation.Execute();
        }
    }
}
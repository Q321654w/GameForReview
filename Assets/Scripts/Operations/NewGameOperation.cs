using UnityEngine.SceneManagement;

namespace Operations
{
    public class NewGameOperation
    {
        private const int GAME_SCENE = 0;

        public void Execute()
        {
            SceneManager.LoadScene(GAME_SCENE);
        }
    }
}
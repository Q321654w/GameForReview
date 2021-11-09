using UnityEngine.SceneManagement;

public class NewGameOperation
{
    private const int GAME_SCENE = 0;

    public NewGameOperation()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }
}
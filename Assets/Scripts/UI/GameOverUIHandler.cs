using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIHandler : MonoBehaviour
{
    [SerializeField] private string StartSceneName;
    [SerializeField] private string MainMenuSceneName;

    public void OnNewGameButtonClicked()
    {
        SceneManager.LoadScene(StartSceneName);
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene(MainMenuSceneName);
    }

    public void OnQuitGameButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

}

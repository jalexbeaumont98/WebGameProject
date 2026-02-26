using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] private string StartSceneName;

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene(StartSceneName);
    }

    public void OnContinueButtonClicked()
    {
        Debug.Log("To be implemented...");
    }

    public void OnOptionsButtonClicked()
    {
        Debug.Log("To be implemented...");
    }

    public void OnQuitGameButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}

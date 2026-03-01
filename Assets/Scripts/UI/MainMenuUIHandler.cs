using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] private string StartSceneName;
    [SerializeField] private string OptionsSceneName;

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene(StartSceneName);
    }

    public void OnContinueButtonClicked()
    {
        Debug.Log("To be implemented in next iteration");
    }

    public void OnOptionsButtonClicked()
    {
        SceneManager.LoadScene(OptionsSceneName);
    }

    public void OnQuitGameButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}

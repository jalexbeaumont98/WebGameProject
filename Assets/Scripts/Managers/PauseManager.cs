using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private string mainMenuName;

    private InputAction _pauseAction;
    private bool _isPaused = false;

    private void Awake()
    {
        if (playerInput == null) Debug.LogError($"Missing {typeof(PlayerInput).Name} reference in {typeof(PauseMenuUIHandler).Name}");
        _pauseAction = playerInput.actions["OpenPauseMenu"];
    }

    private void OnEnable() => _pauseAction.performed += OnPauseGame;
    private void OnDisable() => _pauseAction.performed -= OnPauseGame;

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    private void OnPauseGame(InputAction.CallbackContext ctx)
    {
        PauseGame();
    }

    public void PauseGame()
    {
        _isPaused = !_isPaused;
        Time.timeScale = _isPaused ? 0 : 1;

        if (_isPaused)
        {
            pauseMenu.SetActive(true); Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else
        {
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
    }

    public void Quit()
    {
        SceneManager.LoadScene(mainMenuName);
    }
}

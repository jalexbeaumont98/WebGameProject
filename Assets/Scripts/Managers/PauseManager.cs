using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private InputAction _pauseAction;
    private bool _isPaused = false;

    private void Awake()
    {
        if (playerInput == null) Debug.LogError($"Missing {typeof(PlayerInput).Name} reference in {typeof(PauseMenuUIHandler).Name}");
        _pauseAction = playerInput.actions["OpenPauseMenu"]; 
    }

    private void OnEnable()  => _pauseAction.performed += OnPauseGame;
    private void OnDisable() => _pauseAction.performed -= OnPauseGame;

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    private void OnPauseGame(InputAction.CallbackContext ctx)
    {
        _isPaused = !_isPaused; 
        Time.timeScale = _isPaused ? 0 : 1;
    }
}

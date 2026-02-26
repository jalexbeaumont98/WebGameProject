using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSFXController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private InputAction fireAction;
    private InputAction moveAction;

    private void Awake()
    {
        if (playerInput == null) playerInput = GetComponentInParent<PlayerInput>();
        fireAction = playerInput.actions["Attack"];
        moveAction = playerInput.actions["Rolling/Move"];
    }

    private void OnEnable()
    {
        fireAction.started += OnFirePlaySound;
        moveAction.started += OnMoveStartPlaySound;
        moveAction.canceled += OnMoveStopPlaySound;
    }

    private void OnDisable()
    {
        fireAction.started -= OnFirePlaySound;
        moveAction.started -= OnMoveStartPlaySound;
        moveAction.canceled -= OnMoveStopPlaySound;
    }

    private void OnFirePlaySound(InputAction.CallbackContext ctx)
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager instance not found");
            return;
        }

        AudioManager.Instance.PlayOneShot(SoundType.Gunshot);
    }

    // Pressing shift seems to toggle on and off the sound for movement
    private void OnMoveStartPlaySound(InputAction.CallbackContext ctx)
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager instance not found");
            return;
        }

        AudioManager.Instance.Play(SoundType.Move);
    }

    private void OnMoveStopPlaySound(InputAction.CallbackContext ctx)
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager instance not found");
            return;
        }

        AudioManager.Instance.Stop();
    }
}

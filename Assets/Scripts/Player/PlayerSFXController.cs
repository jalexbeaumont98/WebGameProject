using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSFXController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PlayerInput playerInput;

    private InputAction fireAction;
    private InputAction moveAction;

    private void Awake()
    {
        if (playerInput == null) playerInput = GetComponentInParent<PlayerInput>();
        fireAction = playerInput.actions["Attack"];
        moveAction = playerInput.actions["Rolling/Move"];
        // moveAction = GetComponent<PlayerInput>().actions["Rolling/Move"];
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

    private void OnMoveStartPlaySound(InputAction.CallbackContext ctx)
    {
        Debug.LogError("OnMoveStartCalled");
        if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager instance not found");
            return;
        }
        Debug.LogError("OnMoveStartCalled 2");
        AudioManager.Instance.Play(SoundType.Move);
    }

    private void OnMoveStopPlaySound(InputAction.CallbackContext ctx)
    {
        if (AudioManager.Instance == null)
        {
            Debug.LogError("AudioManager instance not found");
            return;
        }

        Debug.LogError("OnMoveStopCalled");
        AudioManager.Instance.Stop();
    }
}

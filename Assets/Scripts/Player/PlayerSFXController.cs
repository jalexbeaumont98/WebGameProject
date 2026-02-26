using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSFXController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PlayerInput playerInput;

    [Header("SoundFX")]
    [SerializeField] private SoundType soundType;

    private InputAction fireAction;
    private void Awake()
    {
        if (playerInput == null) playerInput = GetComponentInParent<PlayerInput>();
        fireAction = playerInput.actions["Attack"]; // ensure Fire exists in TPS map
    }

    private void OnEnable()
    {
        fireAction.started += OnFirePlaySound;
    }

    private void OnDisable()
    {
        fireAction.started -= OnFirePlaySound;
    }

    private void OnFirePlaySound(InputAction.CallbackContext ctx)
    {
        AudioManager.Instance.PlayOneShot(soundType);
    }
}

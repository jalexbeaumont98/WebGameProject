using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBombController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Bomb")]
    [SerializeField] private GameObject bombPrefab;

    private InputAction dropBombAction;
    
    private void Awake()
    {
        if (playerInput == null) playerInput = GetComponentInParent<PlayerInput>();
        dropBombAction = playerInput.actions["DropBomb"];
    }

    private void OnEnable()
    {
        dropBombAction.performed += OnDropBomb;
    }

    private void OnDisable()
    {
        dropBombAction.performed -= OnDropBomb;
    }

    private void OnDropBomb(InputAction.CallbackContext _)
    {
        if (bombPrefab == null) return;

        if (InventorySystem.Instance.CanConsume(ItemType.Bomb))
        {
            InventorySystem.Instance.Consume(ItemType.Bomb);
            Instantiate(bombPrefab, transform.position, transform.rotation);
            EventChannelManagerB.Instance.BombsDroppedEvent.RaiseEvent(); // For achievement system
        }
    }
}

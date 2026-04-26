using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthConsumer : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerHealthController playerHealth;

    private InputAction useHealAction;
    
    private void Awake()
    {
        if (playerInput == null) playerInput = GetComponentInParent<PlayerInput>();
        useHealAction = playerInput.actions["UseHeal"];
    }

    private void OnEnable()
    {
        useHealAction.performed += OnConsumeHealthPack;
    }

    private void OnDisable()
    {
        useHealAction.performed -= OnConsumeHealthPack;
    }

    public void OnConsumeHealthPack(InputAction.CallbackContext _)
    {
        if (InventorySystem.Instance.CanConsume(ItemType.HealthPack) && playerHealth.CanHeal())
        {
            playerHealth.Heal(100);
            InventorySystem.Instance.Consume(ItemType.HealthPack);
        }
    }
}

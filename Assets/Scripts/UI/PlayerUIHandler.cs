using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private PlayerHealthController playerHealthController;

    private void Start()
    {
        playerHealthController.OnHealthChanged += UpdateHealth;
    }

    private void OnDestroy()
    {
        playerHealthController.OnHealthChanged -= UpdateHealth;
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
       healthBar.maxValue = maxHealth;
       healthBar.value = currentHealth;
    }
}

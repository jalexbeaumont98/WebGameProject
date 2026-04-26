using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private PlayerHealthController playerHealthController;
    [SerializeField] private VerticalLayoutGroup healthPackContainer; // Displays health packs on right side of ui 
    [SerializeField] private Image healthPackIcon; // Image of the health pack icon to be displayed in ui 

    private void Start()
    {
        playerHealthController.OnHealthChanged += UpdateHealth;
        InventorySystem.Instance.OnHealthPackCountChanged += UpdateHealthPacks;
    }

    private void OnDestroy()
    {
        playerHealthController.OnHealthChanged -= UpdateHealth;
        InventorySystem.Instance.OnHealthPackCountChanged -= UpdateHealthPacks;
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
       healthBar.maxValue = maxHealth;
       healthBar.value = currentHealth;
    }

    public void UpdateHealthPacks(int amount)
    {
        foreach (Transform child in healthPackContainer.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < amount; i++)
        {
            Instantiate(healthPackIcon, healthPackContainer.transform);
        }
    }
}

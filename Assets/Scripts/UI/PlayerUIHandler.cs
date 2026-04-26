using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandler : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private PlayerHealthController playerHealthController;
    [SerializeField] private VerticalLayoutGroup healthPackContainer; // Displays health packs on right side of ui 
    [SerializeField] private Image healthPackIcon; // Image of the health pack icon to be displayed in ui 
    [SerializeField] private TMP_Text bombCount; 
    [SerializeField] private TMP_Text achievementMessage; 
    [SerializeField] private AchievementSystemB achievementSystem;

    private Coroutine _achievementMessageCo;

    private void Start()
    {
        playerHealthController.OnHealthChanged += UpdateHealth;
        InventorySystem.Instance.OnHealthPackCountChanged += UpdateHealthPacks;
        InventorySystem.Instance.OnBombCountChanged += UpdateBombCount;
        achievementSystem.OnAchievementUnlocked += DisplayAchievementMessage;
    }

    private void OnDestroy()
    {
        playerHealthController.OnHealthChanged -= UpdateHealth;
        InventorySystem.Instance.OnHealthPackCountChanged -= UpdateHealthPacks;
        InventorySystem.Instance.OnBombCountChanged -= UpdateBombCount;
        achievementSystem.OnAchievementUnlocked -= DisplayAchievementMessage;
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

    public void UpdateBombCount(int amount)
    {
        bombCount.text = "Bombs: #" + amount;
    }

    public void DisplayAchievementMessage(string message)
    {
        if (_achievementMessageCo != null)
        {
            StopCoroutine(_achievementMessageCo);
            _achievementMessageCo = null;
        }

        _achievementMessageCo = StartCoroutine(DisplayMessageFor(message, 3f));
    }

    public IEnumerator DisplayMessageFor(string message, float duration)
    {
        achievementMessage.text = "Achievement Unlocked:\n" + message;
        yield return new WaitForSeconds(duration);
        achievementMessage.text = "";
    }
}

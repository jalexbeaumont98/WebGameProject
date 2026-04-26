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

    // [SerializeField] private TMP_Text flashMessage; 

    // private Coroutine _displayMessageCo;

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

    public void UpdateBombCount(int amount)
    {
        bombCount.text = "Bombs: #" + amount;
    }

    // public void DisplayMessage(string message, float duration)
    // {
    //     if (_displayMessageCo != null)
    //     {
    //         StopCoroutine(_displayMessageCo);
    //         _displayMessageCo = null;
    //     }

    //     _displayMessageCo = StartCoroutine(DisplayMessageFor(message, duration));
    // }

    // public IEnumerator DisplayMessageFor(string message, float duration)
    // {
    //     flashMessage.text = message;
    //     yield return new WaitForSeconds(duration);
    //     flashMessage.text = "";
    // }
}

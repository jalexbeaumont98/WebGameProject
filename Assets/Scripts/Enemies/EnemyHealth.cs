using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;

    [SerializeField] private FlashDamageFX flashDamageFX; // Makes player appear red when damaged (you can delete this; I was only really using it for testing)

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount, Vector3 hitPoint, Vector3 hitDirection)
    {
        currentHealth -= amount;

        Debug.Log(name + " took damage: " + amount);

        flashDamageFX.Play(); 

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth(int value)
    {
        currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }
}

using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private FlashDamageFX flashDamageFX; 
    [SerializeField] private GameObject destroyedPrefabFX; 

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
        GameObject enemyRubble = Instantiate(destroyedPrefabFX, transform.position, Quaternion.identity);
        Destroy(enemyRubble, 3.5f);

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

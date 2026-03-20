using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;

    [SerializeField] private FlashDamageFX flashDamageFX; // Makes player appear red when damaged (you can delete this; I was only really using it for testing)

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount, Vector3 hitPoint, Vector3 hitDirection)
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
}

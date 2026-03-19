using System;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamageable
{
    public event Action<float, float> OnHealthChanged; // This is used by the PlayerUIHandler/Manager to update the health bar.

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private FlashDamageFX flashDamageFX; // Makes player appear red when damaged (you can delete this; I was only really using it for testing)

    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;

        OnHealthChanged?.Invoke(_currentHealth, maxHealth);
    }

    public void TakeDamage(float amount, Vector3 hitPoint, Vector3 hitDirection)
    {
        _currentHealth -= amount;

        flashDamageFX.Play(); // Flash red when damaged (you can delete this; was just for testing)

        OnHealthChanged?.Invoke(_currentHealth, maxHealth); // Update health bar again

        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.LoadDeathSequence();
    }

}

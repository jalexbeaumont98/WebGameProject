using System;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamageable, IHealable
{
    public event Action<float, float> OnHealthChanged; // This is used by the PlayerUIHandler/Manager to update the health bar.

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private FlashDamageFX flashDamageFX; // Makes player appear red when damaged (you can delete this; I was only really using it for testing)

    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;

        OnHealthChanged?.Invoke(_currentHealth, maxHealth);
    }

    public void TakeDamage(int amount, Vector3 hitPoint, Vector3 hitDirection)
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

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public void SetCurrentHealth(int value)
    {
        _currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }

    public void Heal(int amount)
    {
        if (_currentHealth < maxHealth)
        {
            int previousHealth = _currentHealth;
            _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
            OnHealthChanged?.Invoke(_currentHealth, maxHealth); 
        }
    }

    public bool CanHeal()
    {
        return _currentHealth != maxHealth;
    }
}

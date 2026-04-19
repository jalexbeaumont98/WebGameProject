using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable, IMarkable
{
    public event Action Died;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private FlashDamageFX flashDamageFX; 
    [SerializeField] private GameObject destroyedPrefabFX; 

    private int currentHealth;
    private MarkType _mark = MarkType.None; // The player will mark the enemy if their bullet does damage to them. 

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Mark(MarkType mark)
    {
        _mark = mark;
    }

    public void TakeDamage(int amount, Vector3 hitPoint, Vector3 hitDirection)
    {
        currentHealth -= amount;

        flashDamageFX.Play(); 

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth(int value)
    {
        currentHealth = Mathf.Clamp(value, 0, maxHealth);
    }

    private void Die()
    {
        Died?.Invoke();

        // Only call this event if the player damages the enemy
        if (_mark != MarkType.None && _mark == MarkType.Player) 
            EventChannelManager.Instance.EnemyDefeatedEvent.RaiseEvent();

        GameObject enemyRubble = Instantiate(destroyedPrefabFX, transform.position, Quaternion.identity);
        Destroy(enemyRubble, 3.5f);

        Destroy(gameObject);
    }

}
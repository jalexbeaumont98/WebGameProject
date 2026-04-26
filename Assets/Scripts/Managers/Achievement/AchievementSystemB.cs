using UnityEngine;
using System;

public class AchievementSystemB : MonoBehaviour
{
    public event Action<string> OnAchievementUnlocked; 

    [SerializeField] private EnemyDefeatedEventChannel enemyDefeatedChannel;
    [SerializeField] private BulletsFiredEventChannel bulletsFiredChannel;
    [SerializeField] private DamageDealtEventChannel damageDealtChannel;
    [SerializeField] private BombsDroppedEventChannel bombsDroppedChannel;

    [SerializeField] private int achievementEnemiesDefeated = 3;
    [SerializeField] private int achievementBulletsFired = 30;
    [SerializeField] private int achievementDamageDealt = 10;
    [SerializeField] private int achievementBombsDropped = 5;

    private int _currentEnemiesDefeated = 0;
    private int _currentBulletsFired = 0;
    private int _currentDamageDealt = 0;
    private int _currentBombsDropped = 0;

    private void OnEnable()
    {
        enemyDefeatedChannel.OnEventRaised += EnemyDefeatedEvent;
        bulletsFiredChannel.OnEventRaised += BulletsFiredEvent;
        damageDealtChannel.OnEventRaised += DamageDealtEventCalled;
        bombsDroppedChannel.OnEventRaised += BombsDroppedEvent;
    }

    private void OnDisable()
    {
        enemyDefeatedChannel.OnEventRaised -= EnemyDefeatedEvent;
        bulletsFiredChannel.OnEventRaised -= BulletsFiredEvent;
        damageDealtChannel.OnEventRaised -= DamageDealtEventCalled;
        bombsDroppedChannel.OnEventRaised -= BombsDroppedEvent;
    }

    private void EnemyDefeatedEvent()
    {
        // If the player does any damage to the enemy, then the player has defeated the enemy if the enemy dies in any way.
        _currentEnemiesDefeated++;
        if (_currentEnemiesDefeated == achievementEnemiesDefeated)
        {
            Debug.Log("Achievement Unlocked:\n" + _currentEnemiesDefeated + " enemies were defeated.");
            OnAchievementUnlocked?.Invoke(_currentEnemiesDefeated + " enemies were defeated.");
            AudioManager.Instance.PlayUIOneShot(SoundType.AchievementUnlocked);
        }
    }

    private void BulletsFiredEvent()
    {
        _currentBulletsFired++;
        if (_currentBulletsFired == achievementBulletsFired)
        {
            Debug.Log("Achievement Unlocked: " + _currentBulletsFired + " bullets were fired.");
            OnAchievementUnlocked?.Invoke(_currentBulletsFired + " bullets were fired.");
            AudioManager.Instance.PlayUIOneShot(SoundType.AchievementUnlocked);
        }
    }

    private void DamageDealtEventCalled(int damageValue)
    {
        _currentDamageDealt+= damageValue;
        if (_currentDamageDealt == achievementDamageDealt)
        {
            Debug.Log("Achievement Unlocked: " + _currentDamageDealt + " damage dealt.");
            OnAchievementUnlocked?.Invoke(_currentDamageDealt + " damage dealt.");
            AudioManager.Instance.PlayUIOneShot(SoundType.AchievementUnlocked);
        }
    }

    private void BombsDroppedEvent()
    {
        _currentBombsDropped++;
        if (_currentBombsDropped == achievementBombsDropped)
        {
            Debug.Log("Achievement Unlocked: " + _currentBombsDropped + " bombs dropped.");
            OnAchievementUnlocked?.Invoke(_currentBombsDropped + " bombs dropped.");
            AudioManager.Instance.PlayUIOneShot(SoundType.AchievementUnlocked);
        }
    }
}

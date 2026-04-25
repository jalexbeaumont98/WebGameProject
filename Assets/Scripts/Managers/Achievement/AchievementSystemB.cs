using UnityEngine;

public class AchievementSystemB : MonoBehaviour
{
    [SerializeField] private EnemyDefeatedEventChannel enemyDefeatedChannel;
    [SerializeField] private BulletsFiredEventChannel bulletsFiredChannel;
    [SerializeField] private DamageDealtEventChannel damageDealtChannel;

    [SerializeField] private int achievementEnemiesDefeated = 3;
    [SerializeField] private int achievementBulletsFired = 30;
    [SerializeField] private int achievementDamageDealt = 10;

    private int _currentEnemiesDefeated = 0;
    private int _currentBulletsFired = 0;
    private int _currentDamageDealt = 0;

    private void OnEnable()
    {
        enemyDefeatedChannel.OnEventRaised += EnemyDefeatedEvent;
        bulletsFiredChannel.OnEventRaised += BulletsFiredEvent;
        damageDealtChannel.OnEventRaised += DamageDealtEventCalled;
    }

    private void OnDisable()
    {
        enemyDefeatedChannel.OnEventRaised -= EnemyDefeatedEvent;
        bulletsFiredChannel.OnEventRaised -= BulletsFiredEvent;
        damageDealtChannel.OnEventRaised -= DamageDealtEventCalled;
    }

    private void EnemyDefeatedEvent()
    {
        // If the player does any damage to the enemy, then the player has defeated the enemy if the enemy dies in any way.
        _currentEnemiesDefeated++;
        if (_currentEnemiesDefeated == achievementEnemiesDefeated)
        {
            Debug.Log("Achievement Unlocked: " + _currentEnemiesDefeated + " enemies were defeated.");
            AudioManager.Instance.PlayUIOneShot(SoundType.AchievementUnlocked);
        }
    }

    private void BulletsFiredEvent()
    {
        _currentBulletsFired++;
        if (_currentBulletsFired == achievementBulletsFired)
        {
            Debug.Log("Achievement Unlocked: " + _currentBulletsFired + " bullets were fired.");
            AudioManager.Instance.PlayUIOneShot(SoundType.AchievementUnlocked);
        }
    }

    private void DamageDealtEventCalled(int damageValue)
    {
        _currentDamageDealt+= damageValue;
        if (_currentDamageDealt == achievementDamageDealt)
        {
            Debug.Log("Achievement Unlocked: " + _currentDamageDealt + " damage dealt.");
            AudioManager.Instance.PlayUIOneShot(SoundType.AchievementUnlocked);
        }
    }
}

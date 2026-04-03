using UnityEngine;

// The factory's responsibility is to provide a product to the consumer.
// The product that is supplied to the consumer is the enemy.
// Consumer: Spawner
// Product: Enemy
public abstract class EnemyFactory : ScriptableObject
{
    public abstract IEnemy CreateEnemy(Vector3 spawnPosition);
}

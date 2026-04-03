using UnityEngine;

[CreateAssetMenu(fileName = "RangedEnemyFactory", menuName = "Scriptable Objects/RangedEnemyFactory")]
public class RangedEnemyFactory : EnemyFactory
{
    [SerializeField] private RangedEnemy enemyPrefab;

    public override IEnemy CreateEnemy(Vector3 spawnPosition)
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("RangedEnemyFactory: RangedEnemyPrefab is not assigned.");
            return null;
        }

        GameObject enemyGoInstance = Instantiate(enemyPrefab.gameObject, spawnPosition, Quaternion.identity);
        RangedEnemy newEnemy = enemyGoInstance.GetComponent<RangedEnemy>();
        return (IEnemy)newEnemy;
    }
}

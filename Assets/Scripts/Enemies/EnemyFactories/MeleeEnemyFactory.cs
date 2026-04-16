using UnityEngine;

[CreateAssetMenu(fileName = "MeleeEnemyFactory", menuName = "Scriptable Objects/MeleeEnemyFactory")]
public class MeleeEnemyFactory : EnemyFactory
{
    [SerializeField] private MeleeEnemyAI enemyPrefab;

    public override IEnemy CreateEnemy(Vector3 spawnPosition)
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("MeleeEnemyFactory: MeleeEnemyPrefab is not assigned.");
            return null;
        }

        GameObject enemyGoInstance = Instantiate(enemyPrefab.gameObject, spawnPosition, Quaternion.identity);
        IEnemy newEnemy = enemyGoInstance.GetComponent<MeleeEnemyAI>();
        return newEnemy;
    }
}

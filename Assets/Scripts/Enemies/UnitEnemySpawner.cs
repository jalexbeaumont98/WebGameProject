using UnityEngine;

public class UnitEnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyFactory factory;
    // [SerializeField] private MeleeEnemyFactory meleeFactory;
    // [SerializeField] private RangedEnemyFactory rangedFactory;

    void Start()
    {
        // meleeFactory.CreateEnemy(transform.position);
        // rangedFactory.CreateEnemy(transform.position);
        if (factory != null)
        {
            factory.CreateEnemy(transform.position);
        }
    }
}

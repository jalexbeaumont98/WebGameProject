using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private LayerMask damageableLayers;
    [SerializeField] private float radius = 3f;
    [SerializeField] private int damage = 1000;

    private void Start()
    {
        AudioManager.Instance.PlayOneShot(SoundType.Explosion);
        DamageNearbyEnemies();
        Destroy(gameObject, 3.5f);
    }

    private void DamageNearbyEnemies()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, damageableLayers);

        foreach (Collider hit in hits)
        {
            EnemyHealth enemyHealth = hit.GetComponentInParent<EnemyHealth>();

            if (enemyHealth == null) continue;

            Vector3 hitPoint = hit.ClosestPoint(transform.position);
            Vector3 hitDirection = (hit.transform.position - transform.position).normalized;

            enemyHealth.TakeDamage(damage, hitPoint, hitDirection);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

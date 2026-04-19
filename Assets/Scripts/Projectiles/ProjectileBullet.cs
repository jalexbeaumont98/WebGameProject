using UnityEngine;

public class ProjectileBullet : MonoBehaviour
{
    [SerializeField] private string targetTag;
    [SerializeField] private int damage;
    [SerializeField] private GameObject onCollisionPrefabFx; // Particle system creates spark effect on collision

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];

        if (onCollisionPrefabFx != null)
        {
            GameObject sparkFx = Instantiate(onCollisionPrefabFx, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
            Destroy(sparkFx, 1.1f);
        }

        if (collision.transform.CompareTag(targetTag))
        {
            IDamageable damageable = collision.collider.GetComponentInParent<IDamageable>();

            if (damageable != null)
            {
                Vector3 hitPoint = contactPoint.point;
                Vector3 hitDir = collision.relativeVelocity.normalized;
                AudioManager.Instance.PlayOneShot(SoundType.BulletCollision); // Only plays sound when colliding with enemy or else it's too distracting
                damageable.TakeDamage(damage, hitPoint, hitDir);
            }
        }

        ProjectileObjectPool.Instance.ReturnToPool(this); // bullet should disappear if it collides with something no matter what
    }
}

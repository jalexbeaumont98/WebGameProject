using UnityEngine;

public class ProjectileBullet : MonoBehaviour, IMarkable
{
    [SerializeField] private string targetTag;
    [SerializeField] private int damage;
    [SerializeField] private GameObject onCollisionPrefabFx; // Particle system creates spark effect on collision

    private MarkType _mark = MarkType.None; 
    
    // The bullet's mark is none by default, 
    // the player's ShooterController can call Mark to mark the bullet,
    // and the bullet passes on the player's mark to whatever it hits.
    // Enemies marked by the player and die will trigger the EnemyDefeatedEvent
    public void Mark(MarkType mark)
    {
        _mark = mark;
    }

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
            IMarkable markable = collision.collider.GetComponentInParent<IMarkable>();

            markable?.Mark(_mark);

            if (damageable != null)
            {
                Vector3 hitPoint = contactPoint.point;
                Vector3 hitDir = collision.relativeVelocity.normalized;
                AudioManager.Instance.PlayDistantOneShot(SoundType.BulletCollision); // Only plays sound when colliding with enemy or else it's too distracting
                damageable.TakeDamage(damage, hitPoint, hitDir);
                EventChannelManager.Instance.DamageDealtEvent.RaiseEvent(damage);
            }
        }

        ProjectileObjectPool.Instance.ReturnToPool(this); // bullet should disappear if it collides with something no matter what
    }
}

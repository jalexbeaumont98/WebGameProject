using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "Player")
        {
            IDamageable damageable = collision.collider.GetComponentInParent<IDamageable>();

            if (damageable != null)
            {
                Vector3 hitPoint = collision.contacts[0].point;

                damageable.TakeDamage(1f, hitPoint);
            }
        }

        Destroy(gameObject);
    }
}

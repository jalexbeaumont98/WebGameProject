using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private string targetTag;
    [SerializeField] private float damage;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == targetTag)
        {

            IDamageable damageable = collision.collider.GetComponentInParent<IDamageable>();

            if (damageable != null)
            {
                Vector3 hitPoint = collision.contacts[0].point;
                Vector3 hitDir = collision.relativeVelocity.normalized;

                damageable.TakeDamage(damage, hitPoint, hitDir);
            }

            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}

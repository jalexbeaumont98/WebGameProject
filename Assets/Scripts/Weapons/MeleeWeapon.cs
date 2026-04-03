using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject onCollisionPrefabFx; // Particle system creates spark effect

    private bool _wasDetached;

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        if (onCollisionPrefabFx != null)
        {
            GameObject sparkFx = Instantiate(onCollisionPrefabFx, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
            Destroy(sparkFx, 1.0f);
        }
    }

    private void OnTransformParentChanged()
    {
        // add rigid bodies when detached from parent
        if (!_wasDetached && transform.parent == null)
        {
            _wasDetached = true;
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;

            Vector3 dir = (transform.position - transform.position).normalized;
            rb.AddForce(dir * 16f, ForceMode.Impulse);
        }
    }
}

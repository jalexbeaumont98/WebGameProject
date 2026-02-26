using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // TODO: damage logic later
        Destroy(gameObject);
    }
}

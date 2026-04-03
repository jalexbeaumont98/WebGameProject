using UnityEngine;

public class MeleeEnemy : MonoBehaviour, IEnemy
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform orbitPivot;
    [SerializeField] private float orbitIdleSpeed = 90f; // Speed of the sphere's rotation when idling
    [SerializeField] private float orbitAttackSpeed = 900f; // Speed of the sphere's rotation when attacking


    private float _currentOrbitSpeed;

    void Start()
    {
        _currentOrbitSpeed = orbitIdleSpeed;
    }

    void Update()
    {
        if (orbitPivot != null)
        {
            orbitPivot.Rotate(0f, _currentOrbitSpeed * Time.deltaTime, 0f);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.collider.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            SpinAttack();
        }
    }

    public void Attack()
    {
        SpinAttack();
    }

    private void SpinAttack()
    {
        _currentOrbitSpeed = orbitAttackSpeed;
    }
}
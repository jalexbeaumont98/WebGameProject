using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAI : MonoBehaviour, IEnemy
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform orbitPivot;
    [SerializeField] private float orbitIdleSpeed = 90f; // Speed of the sphere's rotation when idling
    [SerializeField] private float orbitAttackSpeed = 900f; // Speed of the sphere's rotation when attacking

    private float _currentOrbitSpeed;
    private EnemyHealth _enemyHealth;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        if (_enemyHealth != null)
            _enemyHealth.Died += ReleaseSpheres;
    }

    private void OnDisable()
    {
        if (_enemyHealth != null)
            _enemyHealth.Died -= ReleaseSpheres;
    }

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

    // Detaches the spheres when the enemy is destroyed
    private void ReleaseSpheres()
    {
        List<Transform> spheres = new List<Transform>();

        for (int i = 0; i < orbitPivot.childCount; i++) spheres.Add(orbitPivot.GetChild(i));

        foreach (Transform sphere in spheres) sphere.SetParent(null, true);

        Destroy(orbitPivot.gameObject);
    }
}

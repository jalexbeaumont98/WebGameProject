using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private float damageAmount; 

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable player = collision.gameObject.GetComponent<IDamageable>();
        player?.TakeDamage(damageAmount, new Vector3(), new Vector3());
    }
}

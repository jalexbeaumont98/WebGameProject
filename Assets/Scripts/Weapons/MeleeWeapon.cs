using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject sphereFracturesPrefab; 
    [SerializeField] private float _explosionForce = 200f;
    [SerializeField] private float _explosionRadius = 5f;
    // [SerializeField] private float randomTorque = 8f;

    private bool _wasDetached;


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

            Invoke(nameof(Explode), 2f);
        }
    }

    private void Explode()
    {
        GameObject sphereFractures = Instantiate(sphereFracturesPrefab, transform.position, transform.rotation);
        // GameObject sphereFractures = Instantiate(sphereFracturesPrefab);
        // sphereFractures.transform.SetPositionAndRotation(transform.position, transform.rotation);
        // Rigidbody[] pieces = sphereFractures.GetComponentsInChildren<Rigidbody>();

        // Vector3 explosionCenter = transform.position;

        // foreach (Rigidbody pieceRb in pieces)
        // {
        //     pieceRb.isKinematic = false;
        //     pieceRb.useGravity = true;
        // Rigidbody pieceRb = GetComponent<Rigidbody>();

        //     pieceRb.AddExplosionForce(
        //         _explosionForce,
        //         transform.position,
        //         _explosionRadius
        //     );

            // pieceRb.AddTorque(Random.insideUnitSphere * randomTorque, ForceMode.Impulse);
        // }
    Debug.Log($"old sphere: {transform.position}");
    Debug.Log($"fracture root: {sphereFractures.transform.position}");
        Destroy(gameObject);
    }
}

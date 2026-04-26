using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Transform cameraTransform;  // your main camera
    [SerializeField] private Transform muzzle;           // spawn point

    [Header("Aim")]
    [SerializeField] private float aimMaxDistance = 200f;
    [SerializeField] private LayerMask aimMask = ~0; // what the aim ray can hit

    [Header("Projectile")]
    [SerializeField] private float projectileSpeed = 45f;
    [SerializeField] private float fireCooldown = 0.12f;

    [Header("Optional")]
    [SerializeField] private Rigidbody ownerRb; // player rb for inherited velocity (feels good)

    [Header("Bomb")]
    [SerializeField] private GameObject bombPrefab;

    private InputAction fireAction;
    private InputAction dropBombAction;
    private float nextFireTime;

    private void Awake()
    {
        if (playerInput == null) playerInput = GetComponentInParent<PlayerInput>();
        fireAction = playerInput.actions["Attack"]; // ensure Fire exists in TPS map
        dropBombAction = playerInput.actions["DropBomb"];
    }

    private void OnEnable()
    {
        fireAction.performed += OnFire;
        dropBombAction.performed += OnDropBomb;
        fireAction.Enable();
    }

    private void OnDisable()
    {
        fireAction.performed -= OnFire;
        dropBombAction.performed -= OnDropBomb;
        fireAction.Disable();
    }

    private void OnFire(InputAction.CallbackContext ctx)
    {
        if (cameraTransform == null || muzzle == null) return;

        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireCooldown;

        // 1) Ray from camera center (camera forward)
        Ray ray = new(cameraTransform.position, cameraTransform.forward);

        Vector3 aimPoint;
        if (Physics.Raycast(ray, out RaycastHit hit, aimMaxDistance, aimMask, QueryTriggerInteraction.Ignore))
        {
            aimPoint = hit.point;
        }
        else
        {
            aimPoint = ray.origin + ray.direction * aimMaxDistance;
        }

        // 2) Direction from muzzle to aim point
        Vector3 dir = (aimPoint - muzzle.position).normalized;

        // 3) Get projectile from object pool and set velocity toward aim point
        ProjectileBullet projectileBullet = ProjectileObjectPool.Instance.Get();
        projectileBullet.transform.SetPositionAndRotation(muzzle.position, Quaternion.LookRotation(dir, Vector3.up));
        projectileBullet.gameObject.SetActive(true);


        projectileBullet.GetComponent<IMarkable>().Mark(MarkType.Player); // Mark the bullet, so bullet passes mark to whatever it hits. If an enemy marked by a player dies, then it contributes to the player's achievement count.

        EventChannelManagerB.Instance.BulletsFiredEvent.RaiseEvent(); // For achievement system

        Rigidbody projRb = projectileBullet.GetComponent<Rigidbody>();
        if (projRb == null)
        {
            Debug.Log("ProjectileBullet is missing rigidbody: player and projectile may not work properly");
            return;
        }

        Vector3 vel = dir * projectileSpeed;
        // Optional: inherit player velocity so shooting while moving feels natural
        if (ownerRb != null) vel += ownerRb.linearVelocity;

        projRb.linearVelocity = vel;
    }

    private void OnDropBomb(InputAction.CallbackContext _)
    {
        if (bombPrefab == null) return;
        Instantiate(bombPrefab, transform.position, transform.rotation);
    }

}

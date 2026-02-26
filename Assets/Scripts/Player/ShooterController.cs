using UnityEngine;
using UnityEngine.InputSystem;

public class ShooterController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Transform cameraTransform;  // your main camera
    [SerializeField] private Transform muzzle;           // spawn point
    [SerializeField] private Rigidbody projectilePrefab; // must have Rigidbody

    [Header("Aim")]
    [SerializeField] private float aimMaxDistance = 200f;
    [SerializeField] private LayerMask aimMask = ~0; // what the aim ray can hit

    [Header("Projectile")]
    [SerializeField] private float projectileSpeed = 45f;
    [SerializeField] private float fireCooldown = 0.12f;

    [Header("Optional")]
    [SerializeField] private Rigidbody ownerRb; // player rb for inherited velocity (feels good)

    private InputAction fireAction;
    private float nextFireTime;

    private void Awake()
    {
        if (playerInput == null) playerInput = GetComponentInParent<PlayerInput>();
        fireAction = playerInput.actions["Attack"]; // ensure Fire exists in TPS map
    }

    private void OnEnable()
    {
        fireAction.performed += OnFire;
        fireAction.Enable();
    }

    private void OnDisable()
    {
        fireAction.performed -= OnFire;
        fireAction.Disable();
    }

    private void OnFire(InputAction.CallbackContext ctx)
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireCooldown;

        if (cameraTransform == null || muzzle == null || projectilePrefab == null) return;

        // 1) Ray from camera center (camera forward)
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

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

        // 3) Spawn projectile and set velocity toward aim point
        Rigidbody proj = Instantiate(projectilePrefab, muzzle.position, Quaternion.LookRotation(dir, Vector3.up));

        Vector3 vel = dir * projectileSpeed;

        // Optional: inherit player velocity so shooting while moving feels natural
        if (ownerRb != null)
            vel += ownerRb.linearVelocity;

        proj.linearVelocity = vel;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class TPSMovementController : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform cam;      // Main Camera
    [SerializeField] private Transform bodyYaw;  // Child transform you rotate for aiming (NOT the sphere)

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;          // target planar speed
    [SerializeField] private float acceleration = 25f;      // how fast we reach target speed
    [SerializeField] private float airControlMultiplier = 0.35f;

    [Header("Jump")]
    [SerializeField] private float jumpImpulse = 6.5f;
    [SerializeField] private float coyoteTime = 0.10f;

    private ModeController modeController;

    private Rigidbody rb;
    private PlayerInput playerInput;

    private InputAction moveAction;
    private InputAction jumpAction;

    Vector2 move;
    bool jumpPressed;

    private float lastGroundedTime;
    private bool jumpQueued;

    public void InitializeM(Rigidbody rb)
    {
        this.rb = rb;
    }

    private void Awake()
    {
        modeController = GetComponent<ModeController>();
        cam = modeController.CamTransform;

        playerInput = modeController.PlayerInput;

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    private void Start()
    {

    }

    public void Update()
    {
        move = moveAction.ReadValue<Vector2>();
        if (jumpAction.WasPressedThisFrame())
            jumpQueued = true;
    }
    private void FixedUpdate()
    {
        if (cam == null) return;

        // --- Ground check ---
        bool grounded = modeController.GroundCheck.IsGrounded;
        if (grounded) lastGroundedTime = Time.time;

        // --- Input ---


        // --- Camera-relative directions (ignore pitch) ---
        Vector3 camForward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized;

        Vector3 desiredDir = (camForward * move.y + camRight * move.x);
        if (desiredDir.sqrMagnitude > 1f) desiredDir.Normalize();

        // Rotate bodyYaw to face movement direction (classic TPS feel)
        if (bodyYaw != null && desiredDir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(desiredDir, Vector3.up);
            bodyYaw.rotation = Quaternion.Slerp(bodyYaw.rotation, targetRot, 0.2f);
        }

        // --- Planar velocity control (physics-friendly “character” feel) ---
        Vector3 v = rb.linearVelocity;
        Vector3 planar = new Vector3(v.x, 0f, v.z);

        float control = grounded ? 1f : airControlMultiplier;

        Vector3 targetPlanarVel = desiredDir * moveSpeed;
        Vector3 velDelta = targetPlanarVel - planar;

        Vector3 platformVel = Vector3.zero;

        if (modeController.GroundCheck.IsGrounded && modeController.GroundCheck.GroundCollider.collider.TryGetComponent(out MovingPlatform platform))
        {
            platformVel = platform.GetPlatformVelocity();
        }

        velDelta += new Vector3(platformVel.x, 0f, platformVel.z);

        // Convert velocity delta to an acceleration force
        Vector3 accelForce = velDelta * (acceleration * control);
        rb.AddForce(accelForce, ForceMode.Acceleration);

        // --- Jump (with small coyote time) ---
        bool canJump = grounded || (Time.time - lastGroundedTime) <= coyoteTime;
        if (jumpQueued && canJump)
        {
            jumpQueued = false; // consume it

            if (rb.linearVelocity.y < 0f)
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
            lastGroundedTime = -999f;
        }
    }
}

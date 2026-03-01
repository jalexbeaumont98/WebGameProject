using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference moveAction; // Vector2: x=steer, y=throttle

    [Header("Grounding")]
    [SerializeField] private float groundRayLength = 0.6f;
    [SerializeField] private LayerMask groundMask = ~0;

    [Header("Movement")]
    [SerializeField] private float acceleration = 35f;     // forward force
    [SerializeField] private float maxSpeed = 25f;
    [SerializeField] private float airMult = 0.35f;

    [SerializeField] private Transform cam; // drag Main Camera here

    private Rigidbody rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void OnEnable()
    {
        if (moveAction != null)
        {
            moveAction.action.Enable();
            moveAction.action.performed += OnMove;
            moveAction.action.canceled += OnMove;
        }
    }

    private void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.action.performed -= OnMove;
            moveAction.action.canceled -= OnMove;
            moveAction.action.Disable();
        }
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }


    void FixedUpdate()
    {
        float steer = moveInput.x;     // A/D
        float throttle = moveInput.y;  // W/S

        float surfaceMult = IsGrounded() ? 1f : airMult;

        Vector3 camForward = Vector3.ProjectOnPlane(cam.transform.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(cam.transform.right, Vector3.up).normalized;

        Vector3 moveDir = camForward * throttle + camRight * steer;
        rb.AddForce(moveDir * acceleration, ForceMode.Acceleration);

        if (steer > 0)
        {
            rb.AddForce(camRight * acceleration * surfaceMult, ForceMode.Acceleration);
        }

        else if (steer < 0)
        {
            rb.AddForce(-camRight * acceleration * surfaceMult, ForceMode.Acceleration);
        }

        if (throttle > 0)
        {
            rb.AddForce(camForward * acceleration * surfaceMult, ForceMode.Acceleration);
        }

        else if (throttle < 0)
        {
            rb.AddForce(-camForward * acceleration * surfaceMult, ForceMode.Acceleration);
        }

        Vector3 v = rb.linearVelocity;
        Vector3 planar = new Vector3(v.x, 0f, v.z);
        if (planar.magnitude > maxSpeed)
        {
            Vector3 clamped = planar.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(clamped.x, v.y, clamped.z);
        }

    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundRayLength, groundMask, QueryTriggerInteraction.Ignore);
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }
}

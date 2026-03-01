using UnityEngine;
using UnityEngine.InputSystem;

public class RollingMovementController : MonoBehaviour
{
    [Header("Input")]
    private InputAction moveAction;

    [Header("Movement")]
    [SerializeField] private float acceleration = 35f;     // forward force
    [SerializeField] private float maxSpeed = 25f;
    [SerializeField] private float airMult = 0.35f;

    [SerializeField] private Transform cam; // drag Main Camera here

    private Rigidbody rb;
    private Vector2 moveInput;

    private ModeController modeController;

    public void InitializeM(Rigidbody rb)
    {
        this.rb = rb; 
    }

    void Awake()
    {
        moveAction = GetComponent<PlayerInput>().actions["Rolling/Move"];
    }

    private void Start()
    {
        modeController = GetComponent<ModeController>();
        cam = modeController.CamTransform;
    }

    private void OnEnable()
    {
        if (moveAction != null)
        {
            moveAction.Enable();
            moveAction.performed += OnMove;
            moveAction.canceled += OnMove;
        }
    }

    private void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.performed -= OnMove;
            moveAction.canceled -= OnMove;
            moveAction.Disable();
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

        float surfaceMult = modeController.GroundCheck.IsGrounded ? 1f : airMult;

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

}

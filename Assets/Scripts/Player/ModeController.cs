using UnityEngine;
using UnityEngine.InputSystem;

public class ModeController : MonoBehaviour
{
    private IModeState currentState;

    // These will hold references to your states
    private RollingState rollingState;
    private ThirdPersonState thirdPersonState;
    private HelicopterState helicopterState;

    [Header("Movement")]
    [SerializeField] private RollingMovementController rolling;
    [SerializeField] private TPSMovementController tps;
    [SerializeField] private HelicopterMovementController heli;

    [Header("Combat")]
    [SerializeField] private ShooterController shooter;

    [Header("Camera")]
    [SerializeField] private PlayerCameraController camController;
    [SerializeField] private Transform camTransform;
    public Transform CamTransform => camTransform;

    [Header("Input")]
    [SerializeField] private PlayerInput playerInput;
    public PlayerInput PlayerInput => playerInput;


    [SerializeField] private PlayerGroundCheck groundCheck;
    public PlayerGroundCheck GroundCheck => groundCheck;

    private Rigidbody rb;
    private Vector2 moveInput;

    void Awake()
    {
        // Create state instances and pass this controller into them
        rollingState = new RollingState(this);
        thirdPersonState = new ThirdPersonState(this);
        helicopterState = new HelicopterState(this);

        // Start in Rolling mode (change if needed)
        SwitchState(rollingState);


        rb = GetComponent<Rigidbody>();
        //playerInput = GetComponent<PlayerInput>();


        rolling.InitializeM(rb);
        tps.InitializeM(rb);
        heli.InitializeM(rb);
    }

    void Update()
    {
        currentState?.Tick();
    }

    void FixedUpdate()
    {
        currentState?.FixedTick();
    }

    public void SwitchState(IModeState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;

        if (currentState != null)
            currentState.Enter();
    }

    // Public methods so states can request transitions

    public void SwitchToRolling()
    {
        SwitchState(rollingState);
    }

    public void SwitchToThirdPerson()
    {
        SwitchState(thirdPersonState);
    }

    public void SwitchToHelicopter()
    {
        SwitchState(helicopterState);
    }

    public void SetModeRolling()
    {
        rolling.enabled = true;
        tps.enabled = false;
        heli.enabled = false;

        shooter.enabled = false;              


        camController.SetAllowPitch(false);                                       //cam.AllowPitch = false;               // rolling rule


    }

    public void SetModeTPS()
    {

        rolling.enabled = false;
        tps.enabled = true;
        heli.enabled = false;

        shooter.enabled = true; 


        camController.SetAllowPitch(true);
    }

    public void SetModeHelicopter()
    {
        

    }

    


}

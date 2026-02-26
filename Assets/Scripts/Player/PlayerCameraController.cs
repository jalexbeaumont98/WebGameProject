using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
     [SerializeField] private Transform target;
    [SerializeField] private float orbitSensitivity = 120f;
    [SerializeField] private float pitchSensitivity = 120f;
    [SerializeField] private float pitch = 20f; // fixed downward tilt

    private bool allowPitch = false; // state machine will control this

    [SerializeField] private float minPitch = -40f;
    [SerializeField] private float maxPitch = 70f;


    private InputAction look;

    private float yaw;
    private float currentPitch;
    private float distance;
    private float height;

    private PlayerInput playerInput;

    void Start()
    {
        // Get look action
        playerInput = target.GetComponent<PlayerInput>();
        look = playerInput.actions["Look"];

        // Calculate starting offset values
        Vector3 offset = transform.position - target.position;
        distance = new Vector3(offset.x, 0f, offset.z).magnitude;
        height = offset.y;

        yaw = transform.eulerAngles.y;
        currentPitch = transform.eulerAngles.x;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    void Update()
    {
        Vector2 readLook = look.ReadValue<Vector2>();

        // Only horizontal orbit
        yaw += readLook.x * orbitSensitivity * Time.deltaTime;

        // Only allow pitch if state says so
        if (allowPitch)
        {

            currentPitch -= readLook.y * pitchSensitivity * Time.deltaTime;
            currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);
        }

        else
        {
            currentPitch = pitch;

        } 

        Quaternion rotation = Quaternion.Euler(currentPitch, yaw, 0f);

        Vector3 offset = rotation * new Vector3(0f, 0f, -distance);

        transform.position = target.position + offset + Vector3.up * height;

        transform.LookAt(target.position + Vector3.up * 1f);
    }

    public void SetAllowPitch(bool input)
    {
        allowPitch = input;
    }
}

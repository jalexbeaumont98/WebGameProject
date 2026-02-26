using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float orbitSensitivity = 120f;
    [SerializeField] private float pitch = 20f; // fixed downward tilt

    private InputAction look;

    private float yaw;
    private float distance;
    private float height;

    void Start()
    {
        // Get look action
        look = InputSystem.actions.FindAction("Player/Look");

        // Calculate starting offset values
        Vector3 offset = transform.position - target.position;
        distance = new Vector3(offset.x, 0f, offset.z).magnitude;
        height = offset.y;

        yaw = transform.eulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 readLook = look.ReadValue<Vector2>();

        // Only horizontal orbit
        yaw += readLook.x * orbitSensitivity * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 offset = rotation * new Vector3(0f, 0f, -distance);

        transform.position = target.position + offset + Vector3.up * height;

        transform.LookAt(target.position + Vector3.up * 1f);
    }
}

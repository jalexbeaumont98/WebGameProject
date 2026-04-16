
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerGroundCheck : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private LayerMask groundMask = ~0;
    [SerializeField] private float groundedBuffer = 0.1f;
    [SerializeField] private QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.Ignore;

    [Header("Debug")]
    [SerializeField] private bool showDebug = true;

    public bool IsGrounded { get; private set; }
    public RaycastHit collisionRaycast { get; private set; }

    private SphereCollider sphereCollider;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        Vector3 worldCenter = transform.TransformPoint(sphereCollider.center);

        // Use the collider radius in world space in case the object is scaled.
        float scaledRadius = sphereCollider.radius * Mathf.Max(
            transform.lossyScale.x,
            transform.lossyScale.y,
            transform.lossyScale.z
        );

        // Start slightly above the bottom of the sphere and cast downward a short extra distance.
        float castDistance = groundedBuffer;

        RaycastHit hit;
        IsGrounded = Physics.SphereCast(
            worldCenter,
            scaledRadius * 0.95f,
            Vector3.down,
            out hit,
            castDistance,
            groundMask,
            triggerInteraction
        );

        collisionRaycast = hit;

        if (showDebug)
        {
            DrawDebug(worldCenter, scaledRadius * 0.95f, castDistance);
            print(IsGrounded);
        }
    }

    private void DrawDebug(Vector3 origin, float radius, float distance)
    {
        Color color = IsGrounded ? Color.green : Color.red;

        // Draw main direction line
        Debug.DrawLine(origin, origin + Vector3.down * distance, color);

        // Draw approximate bottom points of sphere at start and end
        Vector3 startBottom = origin + Vector3.down * radius;
        Vector3 endCenter = origin + Vector3.down * distance;
        Vector3 endBottom = endCenter + Vector3.down * radius;

        Debug.DrawLine(startBottom + Vector3.left * 0.2f, startBottom + Vector3.right * 0.2f, color);
        Debug.DrawLine(startBottom + Vector3.forward * 0.2f, startBottom + Vector3.back * 0.2f, color);

        Debug.DrawLine(endBottom + Vector3.left * 0.2f, endBottom + Vector3.right * 0.2f, color);
        Debug.DrawLine(endBottom + Vector3.forward * 0.2f, endBottom + Vector3.back * 0.2f, color);
    }
}

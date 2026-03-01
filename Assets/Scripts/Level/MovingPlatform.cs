using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    [Header("Path")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform root;

    [Header("Movement")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float reachDistance = 0.1f;
    [SerializeField] private bool loop = true;

    private Rigidbody rb;
    private int currentIndex = 0;
    private float fixedY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        fixedY = transform.position.y;

        PopulateWaypointsFromRoot();
    }

    private void FixedUpdate()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentIndex];

        // Only use X and Z from waypoint
        Vector3 targetPos = new Vector3(target.position.x, fixedY, target.position.z);

        Vector3 direction = targetPos - rb.position;
        float distance = direction.magnitude;

        if (distance <= reachDistance)
        {
            currentIndex++;

            if (currentIndex >= waypoints.Length)
            {
                if (loop)
                    currentIndex = 0;
                else
                    currentIndex = waypoints.Length - 1;
            }

            return;
        }

        Vector3 moveDir = direction.normalized;
        Vector3 newPos = rb.position + moveDir * speed * Time.fixedDeltaTime;

        rb.MovePosition(newPos);
    }

    private void PopulateWaypointsFromRoot()
    {
        if (root == null)
        {
            Debug.LogWarning("Root not assigned.");
            return;
        }

        int childCount = root.childCount;

        waypoints = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            waypoints[i] = root.GetChild(i);
        }

        
    }

    public Vector3 GetPlatformVelocity()
    {
        return rb.linearVelocity;
    }
}

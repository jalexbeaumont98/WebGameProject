using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    private enum State { Wander, Chase, Attack }

    [Header("Refs")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Rigidbody projectilePrefab;

    [Header("Detection")]
    [SerializeField] private float aggroRange = 18f;
    [SerializeField] private float attackRange = 12f;
    [SerializeField] private float loseRange = 26f;
    [SerializeField] private float lineOfSightHeight = 1.2f;
    [SerializeField] private LayerMask losMask = ~0;

    [Header("Wander")]
    [SerializeField] private float wanderRadius = 14f;
    [SerializeField] private float wanderRepathTime = 2.0f;

    [Header("Movement (physics)")]
    [SerializeField] private float moveSpeed = 4.5f;      // meters/sec
    [SerializeField] private float accel = 18f;           // how quickly velocity changes
    [SerializeField] private float turnSpeed = 10f;       // slerp speed

    [Header("Shooting")]
    [SerializeField] private float fireCooldown = 0.6f;
    [SerializeField] private float projectileSpeed = 28f;

    private Rigidbody rb;
    private NavMeshAgent agent;
    private State state;

    private float nextWanderTime;
    private float nextFireTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        // Critical for "agent path + rb movement" approach
        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    private void Start()
    {
        // If not assigned, try find player by tag
        if (player == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        state = State.Wander;
        PickNewWanderPoint();
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);

        // --- State transitions ---
        if (state == State.Wander)
        {
            if (dist <= aggroRange) state = State.Chase;
        }
        else if (state == State.Chase)
        {
            if (dist <= attackRange && HasLineOfSight()) state = State.Attack;
            else if (dist >= loseRange) { state = State.Wander; PickNewWanderPoint(); }
        }
        else if (state == State.Attack)
        {
            if (dist > attackRange || !HasLineOfSight()) state = State.Chase;
        }

        // --- Behaviors ---
        if (state == State.Wander)
        {
            if (Time.time >= nextWanderTime || agent.remainingDistance <= 1.0f)
                PickNewWanderPoint();

            MoveAlongAgentPath();
        }
        else if (state == State.Chase)
        {
            agent.SetDestination(player.position);
            MoveAlongAgentPath();
        }
        else // Attack
        {
            // Stop advancing much; keep position but face player
            agent.SetDestination(transform.position);

            FaceTowards(player.position);

            // Fire if off cooldown
            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + fireCooldown;
                FireAtPlayer();
            }
        }

        // Keep agent synced to rb
        agent.nextPosition = rb.position;
    }

    private void PickNewWanderPoint()
    {
        nextWanderTime = Time.time + wanderRepathTime;

        Vector3 random = Random.insideUnitSphere * wanderRadius;
        random.y = 0f;
        Vector3 candidate = transform.position + random;

        if (NavMesh.SamplePosition(candidate, out var hit, wanderRadius, NavMesh.AllAreas))
            agent.SetDestination(hit.position);
    }

    private void MoveAlongAgentPath()
    {
        Vector3 toNext = agent.nextPosition - rb.position;
        toNext.y = 0f;

        // If agent hasn't advanced yet, use desiredVelocity instead
        Vector3 desiredDir = agent.desiredVelocity;
        desiredDir.y = 0f;

        if (desiredDir.sqrMagnitude < 0.001f && toNext.sqrMagnitude > 0.01f)
            desiredDir = toNext.normalized;
        else if (desiredDir.sqrMagnitude > 0.001f)
            desiredDir.Normalize();

        // Accelerate rb toward target planar velocity
        Vector3 v = rb.linearVelocity;
        Vector3 planar = new Vector3(v.x, 0f, v.z);

        Vector3 targetPlanarVel = desiredDir * moveSpeed;
        Vector3 velDelta = targetPlanarVel - planar;

        rb.AddForce(velDelta * accel, ForceMode.Acceleration);

        // Rotate to face movement direction
        if (desiredDir.sqrMagnitude > 0.001f)
            FaceTowards(rb.position + desiredDir);
    }

    private void FaceTowards(Vector3 worldPoint)
    {
        Vector3 dir = worldPoint - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f) return;

        Quaternion target = Quaternion.LookRotation(dir.normalized, Vector3.up);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, target, 1f - Mathf.Exp(-turnSpeed * Time.fixedDeltaTime)));
    }

    private bool HasLineOfSight()
    {
        Vector3 origin = transform.position + Vector3.up * lineOfSightHeight;
        Vector3 target = player.position + Vector3.up * 1.0f;
        Vector3 dir = (target - origin);
        float dist = dir.magnitude;
        dir /= Mathf.Max(dist, 0.0001f);

        // Raycast: if something blocks, no LOS
        if (Physics.Raycast(origin, dir, out var hit, dist, losMask, QueryTriggerInteraction.Ignore))
        {
            // True LOS only if the first thing hit is the player (or a child of it)
            return hit.transform == player || hit.transform.IsChildOf(player);
        }

        return true;
    }

    private void FireAtPlayer()
    {
        if (projectilePrefab == null || muzzle == null) return;

        Vector3 aimPoint = aimTarget.position;
        Vector3 dir = (aimPoint - muzzle.position).normalized;

        print("Shoot maybe?");
        Rigidbody proj = Instantiate(projectilePrefab, muzzle.position, Quaternion.LookRotation(dir, Vector3.up));
        proj.linearVelocity = dir * projectileSpeed; // physics bullet
    }
}

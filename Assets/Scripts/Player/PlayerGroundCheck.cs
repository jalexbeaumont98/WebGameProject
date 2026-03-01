using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] private float checkDistance = 0.6f;
    [SerializeField] private LayerMask groundMask = ~0;

    public bool IsGrounded { get; private set; }
    public RaycastHit collisionRaycast { get; private set; }

    private void Update()
    {
        RaycastHit hit;

        IsGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            out hit,
            checkDistance,
            groundMask,
            QueryTriggerInteraction.Ignore
        );

        collisionRaycast = hit;

        //print(IsGrounded);
    }
}

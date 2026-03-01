using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] private float checkDistance = 0.6f;
    [SerializeField] private LayerMask groundMask = ~0;

    public bool IsGrounded { get; private set; }

    private void Update()
    {
        IsGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            checkDistance,
            groundMask,
            QueryTriggerInteraction.Ignore
        );

        //print(IsGrounded);
    }
}

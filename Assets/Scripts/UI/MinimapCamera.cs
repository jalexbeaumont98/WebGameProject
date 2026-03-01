using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 offset;

    private void Start()
    {
        if (target == null) return;

        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + offset;
    }
}

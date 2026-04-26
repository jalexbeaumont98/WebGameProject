using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private ItemType itemType;

    private void OnTriggerEnter(Collider collider)
    {
        ICollector collector = collider.gameObject.GetComponent<ICollector>();
        if (collector != null && collector.CanCollect(itemType))
        {
            collector.Collect(itemType);
            Destroy(gameObject);
        }
    }
}

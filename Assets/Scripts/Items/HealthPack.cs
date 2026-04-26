using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private ItemType itemType = ItemType.HealthPack;

    private void OnTriggerEnter(Collider collider)
    {
        ICollector collector = collider.gameObject.GetComponent<ICollector>();
        if (collector != null && collector.CanCollect(itemType))
        {
            collector.Collect(itemType);
            Destroy(gameObject);
            return;
        }
    }
}

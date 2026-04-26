using UnityEngine;

// Heals the player instantly when they touch this health item
public class HealthItem : MonoBehaviour
{
    [SerializeField] int amount;
    [SerializeField] private ItemType itemType = ItemType.HealthPack;

    private void OnTriggerEnter(Collider collider)
    {
        IHealable healable = collider.gameObject.GetComponent<IHealable>();
        if (healable != null && healable.CanHeal())
        {
            healable.Heal(amount);
            Destroy(gameObject);
            return; // Return to stop code from running because unity destroys the object at the end of the frame.
        }

        ICollector collector = collider.gameObject.GetComponent<ICollector>();
        if (collector != null && collector.CanCollect(itemType))
        {
            collector.Collect(itemType);
            Destroy(gameObject);
            return;
        }
    }
}

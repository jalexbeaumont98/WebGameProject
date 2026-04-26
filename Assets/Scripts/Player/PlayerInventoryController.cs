using UnityEngine;

// Used by the to add items to their inventory.
public class PlayerInventoryController : MonoBehaviour, ICollector
{
    public void Collect(ItemType itemType)
    {
        InventorySystem.Instance.Add(itemType);
    }

    public bool CanCollect(ItemType itemType)
    {
        return InventorySystem.Instance.CanAdd(itemType);
    }
}

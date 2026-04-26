using UnityEngine;

// Used by the to add items to their inventory.
public class PlayerInventoryController : MonoBehaviour, ICollector
{
    public void Collect(ItemType itemType)
    {
        if (CanCollect(itemType))
        {
            InventorySystem.Instance.Add(itemType);
            AudioManager.Instance.PlayOneShot(SoundType.ItemPickup);
        }
    }

    public bool CanCollect(ItemType itemType)
    {
        return InventorySystem.Instance.CanAdd(itemType);
    }
}

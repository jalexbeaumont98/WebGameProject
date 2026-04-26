using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    private ItemType itemType;
    private int maxItemsAllowed; // Maximum number of items per item type
    private int _itemCount = 0;

    
    public void Add(ItemType itemType)
    {
        if (!CanAdd(itemType)) return;

        _itemCount++;
    }

    public bool CanAdd(ItemType itemType)
    {
        return GetItemCount(itemType) < maxItemsAllowed;
    }

    public int GetItemCount(ItemType itemType)
    {
        return _itemCount;
    }
}

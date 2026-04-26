using System;

public class InventorySystem : PersistenceSingleton<InventorySystem>
{
    public event Action<int> OnHealthPackCountChanged; // This is used by the PlayerUIHandler/Manager to update the inventory

    private int maxItemCount = 3; // Maximum number of items per item type

    private int _healthPacks = 0;
    private int _bombs;
    private int _speedBoosters;

    public void Add(ItemType itemType)
    {
        if (!CanAdd(itemType)) return;

        switch (itemType)
        {
            case ItemType.HealthPack:
            AddHealthPack();
            break;

            case ItemType.SpeedBooster:
            _speedBoosters++;
            break;

            case ItemType.Bomb:
            _bombs++;
            break;

            default:
            break;
        }
    }

    public bool CanAdd(ItemType itemType)
    {
        return GetItemCount(itemType) < maxItemCount;
    }

    public int GetItemCount(ItemType itemType)
    {
        return itemType switch
        {
            ItemType.HealthPack => _healthPacks,
            ItemType.Bomb => _bombs,
            _ => 0, // Should I throw an error instead?
        };
    }

    private void AddHealthPack()
    {
        if (_healthPacks < maxItemCount)
        {
            _healthPacks++;
            OnHealthPackCountChanged?.Invoke(_healthPacks);
        }
    }

    public void ConsumeHealthPack()
    {
        if (_healthPacks > 0)
        {
            _healthPacks--;
            OnHealthPackCountChanged?.Invoke(_healthPacks);
        }
    }
}

using System;

public class InventorySystem : PersistenceSingleton<InventorySystem>
{
    public event Action<int> OnHealthPackCountChanged; // This is used by the PlayerUIHandler/Manager to update the inventory
    public event Action<int> OnBombCountChanged; 

    private readonly int maxHealthPackCount = 3; // Maximum allowable number of health packs
    private readonly int maxBombCount = 20; 

    private int _healthPacks;
    private int _bombs;

    private void Start()
    {
        _healthPacks = 0;
        _bombs = maxBombCount;
        OnHealthPackCountChanged?.Invoke(_healthPacks);
        OnBombCountChanged?.Invoke(_bombs);
    }
    
    public void Add(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.HealthPack:
            AddHealthPack();
            break;

            case ItemType.Bomb:
            AddBomb();
            break;

            default:
            break;
        }
    }

    public bool CanAdd(ItemType itemType)
    {
        return itemType switch
        {
            ItemType.HealthPack => CanAddHealthPack(),
            ItemType.Bomb => CanAddBomb(),
            _ => false,
        };
    }

    public bool CanConsume(ItemType itemType)
    {
        return itemType switch
        {
            ItemType.HealthPack => _healthPacks > 0,
            ItemType.Bomb => _bombs > 0,
            _ => false,
        };
    }

    public void Consume(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.HealthPack:
            ConsumeHealthPack();
            break;

            case ItemType.Bomb:
            ConsumeBomb();
            break;

            default:
            break;
        }
    }

    private bool CanAddHealthPack()
    {
        return GetHealthPackCount() < maxHealthPackCount;
    }

    public int GetHealthPackCount()
    {
        return _healthPacks;
    }

    private void AddHealthPack()
    {
        if (CanAddHealthPack())
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

    private bool CanAddBomb()
    {
        return GetBombCount() < maxBombCount;
    }

    public int GetBombCount()
    {
        return _bombs;
    }

    private void AddBomb()
    {
        if (CanAddBomb())
        {
            _bombs++;
            OnBombCountChanged?.Invoke(_bombs);
        }
    }

    public void ConsumeBomb()
    {
        if (_bombs > 0)
        {
            _bombs--;
            OnBombCountChanged?.Invoke(_bombs);
        }
    }
}

public class InventorySystem : PersistenceSingleton<InventorySystem>
{
    private int _potions;
    private int _bombs;

    public void Add(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Potion:
            _potions++;
            break;

            case ItemType.Bomb:
            _bombs++;
            break;

            default:
            break;
        }
    }
}

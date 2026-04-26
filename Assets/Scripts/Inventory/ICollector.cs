public interface ICollector
{
    public bool CanCollect(ItemType itemType);
    public void Collect(ItemType itemType);
}

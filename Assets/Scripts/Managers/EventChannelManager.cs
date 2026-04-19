public class EventChannelManager : PersistentSingleton<EventChannelManager>
{
    public EnemyDefeatedEventChannel EnemyDefeatedEvent;
    public BulletsFiredEventChannel BulletsFiredEvent;
    public DamageDealtEventChannel DamageDealtEvent;
}
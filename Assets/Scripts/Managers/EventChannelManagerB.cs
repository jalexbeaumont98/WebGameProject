public class EventChannelManagerB : PersistentSingleton<EventChannelManagerB>
{
    public EnemyDefeatedEventChannel EnemyDefeatedEvent;
    public BulletsFiredEventChannel BulletsFiredEvent;
    public DamageDealtEventChannel DamageDealtEvent;
    public BombsDroppedEventChannel BombsDroppedEvent;
}
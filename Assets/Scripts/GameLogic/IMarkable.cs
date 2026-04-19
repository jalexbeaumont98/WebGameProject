public interface IMarkable
{
    // Marks target (e.g.: A player's bullet will mark an enemy, and when the enemy dies, the player gets credit for defeating it)
    public void Mark(MarkType mark);
}

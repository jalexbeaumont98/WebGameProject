using UnityEngine;

public interface IHealable
{
    public void Heal(int amount);
    public bool CanHeal(); // If player is at max health, they cannot pick up the heal item.
}

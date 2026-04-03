using UnityEngine;

public class RangedEnemy : MonoBehaviour, IEnemy
{
    public void Attack()
    {
        FireAtPlayer();
    }

    private void FireAtPlayer()
    {
        Debug.Log("Enemy shoots projectile!!");
    }
}

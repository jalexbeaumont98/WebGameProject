using UnityEngine;

public class RangedEnemy : MonoBehaviour
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

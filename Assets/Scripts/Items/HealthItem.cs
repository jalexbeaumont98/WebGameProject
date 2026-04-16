using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] int amount;

    void OnTriggerEnter(Collider collision)
    {
       IHealable healable = collision.gameObject.GetComponent<IHealable>();
       if (healable != null && healable.CanHeal())
       {
            healable.Heal(amount);
            Destroy(gameObject);
       }
    }
}

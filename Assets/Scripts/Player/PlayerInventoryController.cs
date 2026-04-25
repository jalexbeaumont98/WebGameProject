using UnityEngine;

// Used by the to add items to their inventory.
public class PlayerInventoryController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        ICollectable item = collision.gameObject.GetComponent<ICollectable>();    
        InventorySystem.Instance.Add(item.GetItemType());
    }
}

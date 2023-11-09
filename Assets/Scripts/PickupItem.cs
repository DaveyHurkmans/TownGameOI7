using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Item item;

    public void Pickup()
    {
        InventoryManager.Instance.AddItem(item);
        Destroy(gameObject);
    }
}

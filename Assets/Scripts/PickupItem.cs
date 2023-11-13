using UnityEngine; // Add this line

public class PickupItem : MonoBehaviour
{
    public MayoralNotes mayoralNotes;

    public void Pickup()
    {
        InventoryManager.Instance.AddItem(mayoralNotes);
        Destroy(gameObject);
    }
}

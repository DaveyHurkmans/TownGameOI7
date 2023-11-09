using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private List<Item> inventory = new List<Item>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);
        Debug.Log("Item added to inventory: " + item.itemName);
    }

    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
        Debug.Log("Item removed from inventory: " + item.itemName);
    }

    public bool HasItem(Item item)
    {
        return inventory.Contains(item);
    }
}

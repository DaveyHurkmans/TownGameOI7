using System;
using System.Collections.Generic; // Add this line

[System.Serializable]
public class Item
{
    public string itemName;
    public string description;
    public float weight;
    public List<string> categories = new List<string>();

    // Add more properties as needed
}

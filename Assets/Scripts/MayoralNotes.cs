using UnityEngine;

// Derive from the Item class
[System.Serializable]
public class MayoralNotes : Item
{
    public MayoralNotes()
    {
        itemName = "The Mayoral Notes";
        description = "The secret campaign notes of Mayor Brofessor. Rumor has it, the key to his success lies in a mayo-based strategy. Unfortunately, it seems mayo isn't as popular as he thought. Maybe he needs our help?";
        weight = 0.2f;

        // Add the "Books" and "QuestItem" categories
        categories.Add("Books");
        categories.Add("QuestItem");
    }
}

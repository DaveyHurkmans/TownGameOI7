using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    private Animator animator;

    public enum NPCType
    {
        Person,
        Object
    }

    public NPCType npcType = NPCType.Person;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (npcType == NPCType.Person)
        {
            // This is a person, perform person-specific interactions
            string message = "Guten Tag!";
            ChatBubble.Create(transform, new Vector3(-0.3f, 4.7f, 0f), message);
            animator.SetTrigger("Talk");
        }
        else if (npcType == NPCType.Object)
        {
            // This is an object, perform object-specific interactions
            Debug.Log("Interacted with object!");
        }
    }
}

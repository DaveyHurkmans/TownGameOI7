using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        string message = "Guten Tag!";
        ChatBubble.Create(transform, new Vector3(-0.3f, 4.7f, 0f), message);
        animator.SetTrigger("Talk");
    }
    
}

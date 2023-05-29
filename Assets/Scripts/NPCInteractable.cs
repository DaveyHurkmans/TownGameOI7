using System.Collections;
using System.Collections.Generic;
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
        ChatBubble.Create(transform, new Vector3(-0.3f, 1.7f, 0f), "Hallo daar!");
        animator.SetTrigger("Talk");
    }
}

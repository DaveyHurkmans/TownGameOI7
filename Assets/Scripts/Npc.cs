using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public DialogueTrigger trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Optionally display a prompt or message indicating that the player can interact.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Optionally hide the prompt or message when the player is out of range.
        }
    }

    private void Update()
    {
        if (trigger != null && trigger.IsInRange(transform.position) && Input.GetButtonDown("Submit"))
        {
            trigger.StartDialogue();
        }
    }
}

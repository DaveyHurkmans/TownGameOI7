using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public DialogueTrigger trigger;
    private void onCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.CompareTag("Player") == true)
    trigger.StartDialogue();
    }
}

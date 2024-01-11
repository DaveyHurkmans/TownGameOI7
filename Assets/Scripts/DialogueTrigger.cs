using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    [SerializeField]
    private float interactionRange = 2f;

    private bool isInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    private void Update()
    {
        // Optionally add any additional update logic here.
        if (isInRange && Input.GetButtonDown("Submit"))
        {
            StartDialogue();
        }
    }

    public void StartDialogue()
        {
            FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
            // Disable character movement during dialogue
            FindObjectOfType<Character>().ToggleMovement(false);
        }
    

    public bool IsInRange(Vector3 playerPosition)
    {
        float distance = Vector3.Distance(playerPosition, transform.position);
        return distance <= interactionRange;
    }
}

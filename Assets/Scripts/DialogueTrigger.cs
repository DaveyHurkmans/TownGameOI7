using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    [SerializeField]
    private float interactionRange = 2f;

    private bool isInRange = false;
    private bool isDialogueStarted = false;

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
        if (isInRange && Input.GetButtonDown("Submit") && !isDialogueStarted)
        {
            isDialogueStarted = true;
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
    }

    public void EndDialogue()
    {
        isDialogueStarted = false;
    }

    public bool IsInRange(Vector3 playerPosition)
    {
        float distance = Vector3.Distance(playerPosition, transform.position);
        return distance <= interactionRange;
    }
}

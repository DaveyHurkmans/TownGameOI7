using UnityEngine;
using UnityEngine.UI;
using static LeanTween;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Image avatarImage;
    public Text actorNameText;
    public Text messageText;
    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;

    public static bool isActive = false;
    public static bool isInDialogue = false; 

    void Start()
    {
        dialogueBox.SetActive(false);
        isInDialogue = false;
    }

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        isInDialogue = true; 
        DisplayMessage();
    }

    void DisplayMessage()
    {
        if (activeMessage < currentMessages.Length)
        {
            Message messageToDisplay = currentMessages[activeMessage];
            messageText.text = messageToDisplay.message;

            Actor actorToDisplay = currentActors[messageToDisplay.actorId];
            actorNameText.text = actorToDisplay.name;
            avatarImage.sprite = actorToDisplay.sprite;

            dialogueBox.SetActive(true);
            AnimateTextColor();
        }
        else
        {
            EndDialogue();
        }
    }

    public void PreviousMessage()
    {
        activeMessage--;
        if (activeMessage >= 0)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Already at the first message.");
        }
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isActive = false;
        isInDialogue = false;
        dialogueBox.SetActive(false);
        FindObjectOfType<DialogueTrigger>().EndDialogue(); // Notify DialogueTrigger that dialogue has ended
    }

    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Space))
        {
            NextMessage();
        }
    }

    void AnimateTextColor()
    {
        textColor(messageText.rectTransform, Color.clear, 0);
        textColor(messageText.rectTransform, Color.white, 0.5f);
    }
}

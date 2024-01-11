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
    public static bool isInDialogue = false; // New state to check if in dialogue mode

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
        isInDialogue = true; // Set to true when starting dialogue
        Debug.Log("Started conversation! Loaded messages: " + messages.Length);
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

            // Animate text color using LeanTween
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
            Debug.Log("Conversation ended!");
            isActive = false;
        }
    }


    void EndDialogue()
    {
        Debug.Log("Conversation ended!");
        isActive = false;
        isInDialogue = false;

        // Deactivate the dialogue box when the conversation ends
        dialogueBox.SetActive(false);

        // Allow character movement after dialogue ends
        FindObjectOfType<Character>().ToggleMovement(true);
    }


    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Space))
        {
            NextMessage();
        }
    }

    // Additional Methods:

    void AnimateTextColor()
    {
        // Animate text color using LeanTween
        textColor(messageText.rectTransform, Color.clear, 0);
        textColor(messageText.rectTransform, Color.white, 0.5f);
    }


}

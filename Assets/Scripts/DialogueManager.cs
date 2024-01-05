using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;
    Message[] currentMessages; // Corrected variable name and data type
    Actor[] currentActors;
    int activeMessage = 0;

    public static bool isActive = false;

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        Debug.Log("Started conversation! Loaded messages: " + messages.Length);
    }


    void DisplayMessage()
{
    Message messageToDisplay = currentMessages[activeMessage];
    messageText.text = messageToDisplay.message;  // Corrected variable name
    Actor actorToDisplay = currentActors[messageToDisplay.actorId];
    actorName.text = actorToDisplay.name;
    actorImage.sprite = actorToDisplay.sprite;
}

    public void NextMessage() {
        activeMessage++;
        if (activeMessage < currentMessages.Length) {
            DisplayMessage();
        } else {
            Debug.Log("Conversation eneded!");
            isActive = false;
        }
    }






    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive == true ) {
            NextMessage();
        }
    }
}





// using System.Diagnostics;
// using System.Collections.Specialized;
// using System.Numerics;
// using unityEngine;
// using UnityEngine.UI;

// public class DialogueManager: MonoBehaviour 
// {
// public Image actorImage;
// public Text actorName;
// public Text messageText;
// public RectTransform backgroundBox;
// Message[] currentMessages;
// Actor[] currentActors;
// int activeMessage = 0;

// public static bool isActive = false;
// public void OpenDialogue (Message[] messages, Actor[] actors) {
//     currentMessages = messages;
//     currentActors = actors;
//     activeMessage = 0;
//     isActive= true;
//     Debug.Log("Started conversation! Loaded messages:" + messages.Length); 
//     DisplayMessage(); 
//     backgroundBox.LeanScale()
// }

// void DisplayMessage() {
//     Message messageToDisplay = currentMessages [activeMessage]; 
//     messageText.text = messageToDisplay.message;

//     Actor actorToDisplay = currentActors [messageToDisplay.actorId]; 
//     actorName.text = actorToDisplay.name;
//     actorImage.sprite = actorToDisplay.sprite;
// }




// public void NextMessage() {
//     activeMessage++;
//     if (activeMessage < currentMessages.Length) {
//         DisplayMessage();
//     } else {
//         Debug.Log("Conversation done!");
//         backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
//     }
// }


// void Start()
// {
//     backgroundBox.transform.localScale = Vector3.zero;
// } 


// void Update() 
// {
//     if (Input.GetKeyDown(KeyCode. Space) && isActive == true) {
//         NextMessage();
//     }
// }}
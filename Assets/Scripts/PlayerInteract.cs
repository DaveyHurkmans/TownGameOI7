using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TownGameOI7
{
    public class PlayerInteract : MonoBehaviour
    {
        private bool isInteracting = false;

        private void Update()
        {
            if (!isInteracting && Input.GetButtonDown("Submit"))
            {
                StartCoroutine(Interact());
            }
        }

        IEnumerator Interact()
        {
            isInteracting = true;

            // Add logic to check if a dialogue is active and handle input accordingly
            if (DialogueManager.isActive)
            {
                // If dialogue is active, move to the next message with A button on Xbox controller or Left mouse button on PC
                if (Input.GetButtonDown("NextMessage"))
                {
                    FindObjectOfType<DialogueManager>().NextMessage();
                }
                // If B button on Xbox controller or Right mouse button on PC is pressed, go back one message
                else if (Input.GetButtonDown("PreviousMessage"))
                {
                    FindObjectOfType<DialogueManager>().PreviousMessage();
                }
            }


            // Allow time between button presses
            yield return new WaitForSeconds(0.2f);

            isInteracting = false;
        }
    }
}

using UnityEngine;
using System.Collections;

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

            if (!DialogueManager.isInDialogue) // Check if not in dialogue
            {
                // Handle other interactions
            }

            yield return new WaitForSeconds(0.2f);
            isInteracting = false;
        }
    }
}

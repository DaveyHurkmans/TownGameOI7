using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{
    public TextMeshPro textMeshPro;

    public static void Create(Transform parent, Vector3 localPosition, string text)
    {
        Transform chatBubbleTransform = Instantiate(GameAssets.i.pfChatBubble, parent);
        chatBubbleTransform.localPosition = localPosition;

        ChatBubble chatBubble = chatBubbleTransform.GetComponent<ChatBubble>();
        chatBubble.Setup(text);

        Destroy(chatBubbleTransform.gameObject, 6f);
    }

    private void Setup(string text)
    {
        textMeshPro.text = text;
        Debug.Log("Chat Bubble Text: " + text);
    }
}

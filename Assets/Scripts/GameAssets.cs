using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance;

    public Transform pfChatBubble;

    public static GameAssets i;

    private void Awake()
    {
        instance = this;
        i = this;
    }
}

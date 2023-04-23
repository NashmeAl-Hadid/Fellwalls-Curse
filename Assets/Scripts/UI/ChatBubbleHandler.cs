using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubbleHandler : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform[] npcTransformArray;








    private void Start()
    {
        ChatBubble.Create(playerTransform, new Vector3(3, 3), ChatBubble.IconType.Neutral, "Press Space To Jump");


        Transform npcTransform = npcTransformArray[0];
        string message = "Hello";

        ChatBubble.IconType[] iconArray =
            new ChatBubble.IconType[] { ChatBubble.IconType.Happy, ChatBubble.IconType.Happy, ChatBubble.IconType.Neutral, ChatBubble.IconType.Angry };

        ChatBubble.IconType icon = iconArray[Random.Range(0, iconArray.Length)];
        
        ChatBubble.Create(npcTransform, new Vector3(3, 3), icon, message);

    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble : MonoBehaviour
{


    public static void Create(Transform parent, Vector3 localPosition, IconType iconType, string text)
    {
     Transform chatBubbleTransform =   Instantiate(GameAssets.i.pfChatBubble, parent);
        Vector3 offset =  new Vector3(-5f,-1f,0);
        chatBubbleTransform.localPosition = localPosition + offset;


        chatBubbleTransform.GetComponent<ChatBubble>().Setup(iconType, text);

       Destroy(chatBubbleTransform.gameObject, 4f);
    }

    public enum IconType
    {
        Happy,
        Neutral,
        Angry,
    }

     [SerializeField] private Sprite happyIconSprite;
     [SerializeField] private Sprite neutralyIconSprite;
     [SerializeField] private Sprite angryIconSprite;


    private SpriteRenderer backgroundSpriteRenderer;
    private SpriteRenderer iconSpriteRenderer;
    private TextMeshPro textMeshPro;


    private void Awake()
    {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        iconSpriteRenderer = transform.Find("con").GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }




    private void Setup(IconType iconType, string text)
    {
        textMeshPro.SetText(text);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);

        Vector2 padding = new Vector2(3f, 2f);
        backgroundSpriteRenderer.size =  textSize + padding;

        Vector3 offset = new Vector3(-2f, 0,0);
        backgroundSpriteRenderer.transform.localPosition = new Vector3(backgroundSpriteRenderer.size.x / 2f, 0f) + offset;

        iconSpriteRenderer.sprite = GetIconSprite(iconType);

    }

    private Sprite GetIconSprite(IconType iconType)
    {
        switch (iconType)
        {
            default:
            case IconType.Happy:
                return happyIconSprite;
            case IconType.Neutral:
                return neutralyIconSprite;
            case IconType.Angry:
                return angryIconSprite;
        }
    }


}

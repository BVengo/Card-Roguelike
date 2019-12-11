using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResourceCardDisplay : CardDisplay
{
    [SerializeField]
    Image image;

    [SerializeField]
    GameObject toolBubble;
    
    [SerializeField]
    Image toolImage;

    [SerializeField]
    Text count;

    [SerializeField]
    Color silhouetteColor = Color.black;

    public void Set(ResourceCard card, ResourceCardData data)
    {
        image.sprite = card.Item.Sprite;
        count.text = data.Count.ToString();

        if (data.Modifier != null)
        {
            toolBubble.SetActive(true);

            toolImage.sprite = data.Modifier.Tool.Sprite;

            toolImage.color = Color.white;
        }
        else if (card.ToolType == null)
        {
            toolBubble.SetActive(false);
        }
        else
        {
            toolBubble.SetActive(true);

            toolImage.sprite = card.ToolType.Silhouette;

            toolImage.color = silhouetteColor;
        }
    }

    public override void Refresh(CardInstance instance)
    {
        base.Refresh(instance);

        Set(instance.Definition as ResourceCard, instance.Data as ResourceCardData);
    }
}

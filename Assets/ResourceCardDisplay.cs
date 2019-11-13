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

    public void Set(ResourceCard card, ResourceCardData data)
    {
        image.sprite = card.Item.Sprite;
        count.text = data.Count.ToString();
    }
}

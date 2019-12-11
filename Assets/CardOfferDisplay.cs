using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOfferDisplay : MonoBehaviour
{
    [SerializeField]
    GameCore cardOffers;

    Dictionary<CardInstance, CardDisplay> displays = new Dictionary<CardInstance, CardDisplay>();

    void Awake()
    {
        cardOffers.CardManager.CardAdded += Add;
        cardOffers.CardManager.CardRemoved += Remove;

        int index = 0;

        foreach (CardInstance instance in cardOffers.CardManager.Cards)
        {
            Add(instance, index++);
        }
    }

    void Add(CardInstance instance, int index)
    {
        CardDisplay display = instance.CreateDisplay();

        display.Clicked += Select;

        displays[instance] = display;

        display.transform.SetParent(transform);
        display.transform.SetSiblingIndex(index - 1);
    }

    void Remove(CardInstance instance)
    {
        if (displays.TryGetValue(instance, out CardDisplay display))
        {
            Destroy(display.gameObject);
        }
    }

    void Select(CardInstance instance)
    {
        cardOffers.SelectCard(instance);
    }
}

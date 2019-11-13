using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOfferDisplay : MonoBehaviour
{
    [SerializeField]
    CardOffers cardOffers;

    Dictionary<CardInstance, CardDisplay> displays = new Dictionary<CardInstance, CardDisplay>();

    void Awake()
    {
        cardOffers.Added += Add;
        cardOffers.Removed += Remove;

        foreach (CardInstance instance in cardOffers.Shown)
        {
            Add(instance);
        }
    }

    void Add(CardInstance instance)
    {
        CardDisplay display = instance.CreateDisplay();

        display.Clicked += Select;

        displays[instance] = display;

        display.transform.SetParent(transform);
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
        instance.Definition.Apply(cardOffers, instance.Data);

        cardOffers.Clear();

        cardOffers.Draw(3);
    }
}

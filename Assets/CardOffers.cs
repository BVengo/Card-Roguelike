using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CardInstanceHandler(CardInstance instance);

public class CardOffers : MonoBehaviour
{
    public int Count => cards.Count;

    public IEnumerable<CardInstance> Shown => cards;

    public Inventory Inventory => inventory;

    public event CardInstanceHandler Added;
    public event CardInstanceHandler Removed;

    [SerializeField]
    CardDeck deck;

    [SerializeField]
    Inventory inventory;

    List<CardDefinition> allCards = new List<CardDefinition>();

    List<CardInstance> cards = new List<CardInstance>();

    void Awake()
    {
        deck.Fill(allCards);

        Draw(3);
    }

    public void Draw(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CardDefinition chosen = allCards[Random.Range(0, allCards.Count)];

            Add(chosen);
        }
    }

    public void Add(CardDefinition definition)
    {
        CardInstance instance = new CardInstance(definition);

        cards.Add(instance);

        Added?.Invoke(instance);
    }

    public void Remove(CardInstance instance)
    {
        cards.Remove(instance);

        Removed?.Invoke(instance);
    }

    public void Clear()
    {
        while (cards.Count > 0)
        {
            Remove(cards[0]);
        }
    }
}

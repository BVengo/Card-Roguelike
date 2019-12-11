using System.Collections.Generic;
using UnityEngine;

public class CardManager : GameManager
{
    public int CardCount => cards.Count;

    public IEnumerable<CardInstance> Cards => cards;

    public event CardInstanceHandler CardSelected;
    public event CardInstanceIndexHandler CardAdded;
    public event CardInstanceHandler CardRemoved;

    [SerializeField]
    CardDeck deck;

    List<CardDefinition> allCards = new List<CardDefinition>();

    List<CardInstance> cards = new List<CardInstance>();

    public override void Initialize(GameCore core)
    {
        base.Initialize(core);

        deck.Fill(allCards);
        
        DrawCards(3);
    }

    CardDefinition GetRandomCard()
    {
        return allCards[Random.Range(0, allCards.Count)];
    }

    public void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CardDefinition chosen = GetRandomCard();

            AddCard(chosen);
        }
    }
    
    public void AddCard(CardDefinition definition)
    {
        CardInstance instance = new CardInstance(Core, definition);

        cards.Add(instance);

        CardAdded?.Invoke(instance, cards.Count - 1);
    }

    public void AddCard(CardDefinition definition, int index)
    {
        CardInstance instance = new CardInstance(Core, definition);

        cards.Insert(index, instance);

        CardAdded?.Invoke(instance, index);
    }

    public void RemoveCard(CardInstance instance)
    {
        cards.Remove(instance);

        CardRemoved?.Invoke(instance);
    }

    public void ReplaceCard(CardInstance instance)
    {
        int index = cards.IndexOf(instance);

        RemoveCard(instance);

        AddCard(GetRandomCard(), index);
    }

    public void ClearCards()
    {
        while (cards.Count > 0)
        {
            RemoveCard(cards[0]);
        }
    }

    public void EndRound(CardInstance selected)
    {
        selected.Definition.Apply(Core, selected);

        CardSelected?.Invoke(selected);

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].Definition.EndOfRound(Core, cards[i]);
        }
    }
}

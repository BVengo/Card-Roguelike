using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardDeck : ScriptableObject
{
    [System.Serializable]
    public class CardCount
    {
        public CardDefinition Card => card;

        public int Count => count;

        [SerializeField]
        CardDefinition card;

        [SerializeField]
        int count = 1;
    }

    [SerializeField]
    CardCount[] cardCounts;

    public void Fill(List<CardDefinition> deck)
    {
        for (int i = 0; i < cardCounts.Length; i++)
        {
            for (int j = 0; j < cardCounts[i].Count; j++)
            {
                deck.Add(cardCounts[i].Card);
            }
        }
    }
}
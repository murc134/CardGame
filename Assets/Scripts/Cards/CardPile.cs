using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class CardPile : MonoBehaviour
{
    [SerializeField]
    private Player owner;

    public Player Owner
    {
        get
        {
            return owner;
        }
        set
        {
            owner = value;
            if (owner != null)
            {
                owner.CardPile = this;
            }
        }
    }
    [SerializeField]
    private uint max = 0;

    public uint Max
    {
        get
        {
            return max;
        }
        set
        {
            List<Card> cards = new List<Card>(this.cards);
            max = value;
            cards = new List<Card>();
        }
    }

    [SerializeField]
    private List<Card> cards = new List<Card>();

    private Dictionary<uint, Card> cardDict = new Dictionary<uint, Card>();

    public int Count { get { return cards.Count; } }

    public Card Current
    {
        get
        {
            return cards == null || cards.Count <= 0 ? null : cards[0];
        }
    }

    public void NextCard()
    {
        if (cards == null || cards.Count <= 0)
        {
            return;
        }

        // Remove the first element and add it to the end of the list
        Card firstCard = cards[0];
        cards.RemoveAt(0);
        cards.Add(firstCard);
    }

    public void PreviousCard()
    {
        if (cards == null || cards.Count <= 0)
        {
            return;
        }

        // Remove the last element and add it to the beginning of the list
        Card lastCard = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);
        cards.Insert(0, lastCard);
    }

    public bool Full
    {
        get
        {
            return cards.Count >= Max;
        }
    }

    public uint Add(params IEnumerable<Card>[] takecards)
    {
        uint added = 0;
        foreach (var cards in takecards)
        {
            uint add = addRange(cards); 
            if(add <= 0)
            {
                break;
                
            }
            added += add;
        }
        return added;
    }

    public uint Add(params Card[] takecards)
    {
        return addRange(takecards);
    }
    public uint Remove(params IEnumerable<Card>[] takecards)
    {
        uint removed = 0;
        foreach (var cards in takecards)
        {
            uint rm = removeRange(cards);
            if(rm <= 0)
            {
                break;
            }
            removed += rm;
        }
        return removed;
    }

    public uint Remove(params Card[] takecards)
    {
        return removeRange(takecards);
    }

 

    protected virtual uint addRange(IEnumerable<Card> Cards)
    {
        uint addedCount = 0;
        foreach (var card in Cards)
        {
            bool added = false;
            if (!cardDict.ContainsKey(card.ID))
            {
                if(Max > 0)
                {
                    if(cards.Count < Max)
                    {
                        cards.Add(card);
                        cardDict[card.ID] = card;
                        added = true;
                        addedCount++;
                    }
                }

            }
            if (!added)
            {
                break;
            }
        }
        return addedCount;
    }


    protected virtual uint removeRange(IEnumerable<Card> Cards)
    {
        uint deleted = 0;
        foreach (var card in Cards)
        {
            if (cardDict.ContainsKey(card.ID))
            {
                cards.Remove(card);
                cardDict.Remove(card.ID);
                deleted++;
            }
            if(cards.Count <= 0)
            {
                break;
            }
        }
        return deleted;
    }
    public IEnumerable<Card> Sort()
    {
        // Sort the list
        cards.Sort((card1, card2) => card1.ID.CompareTo(card2.ID));

        // Update the dictionary to reflect the new order of the list
        cardDict = cards.ToDictionary(card => card.ID, card => card);

        return cards; // cards is now sorted
    }
    public IEnumerable<Card> Shuffle()
    {
        // Fisher-Yates shuffle
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            var temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }

        // Rebuild the dictionary from the shuffled list
        cardDict = cards.ToDictionary(card => card.ID, card => card);

        return cards;
    }

    public void Clear()
    {
        cards.Clear();
    }
    public IEnumerable<Card> Get(uint numberOfCards)
    {
        if (numberOfCards > cardDict.Count)
        {
            numberOfCards = (uint)cardDict.Count;
        }

        var cardsToReturn = cards.Skip(Mathf.Max(0, cards.Count - (int)numberOfCards)).ToList();

        foreach (var card in cardsToReturn)
        {
            cardDict.Remove(card.ID);
            cards.Remove(card);
        }

        return cardsToReturn;
    }

    public void Give(uint numberOfCards, CardPile CardPile)
    {
        IEnumerable<Card> cards = Get(numberOfCards);
        foreach(var card in cards)
        {
            CardPile.Add(card);
        }
    }
}
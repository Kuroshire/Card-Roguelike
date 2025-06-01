using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck<T> where T : new()
{
    private readonly List<T> deck = new();

    // --- GETTERS ---
    public int CardsLeft => deck.Count;

    // --- Constructors
    public Deck(int deckSize)
    {
        for (int i = 0; i < deckSize; i++)
        {
            //create a random new card from default Card constructor
            deck.Add(new T());
        }
    }

    public Deck(List<T> cards)
    {
        deck = new();
        cards.ForEach(card => AddCardUnder(card));
    }

    #region Draw Logic
    public T DrawOne()
    {
        if (!CanDraw())
        {
            return default;
        }

        T firstCard = deck[0];
        deck.RemoveAt(0);

        return firstCard;
    }

    public T DrawRandom()
    {
        if (!CanDraw())
        {
            return default;
        }

        T drawnCard = deck[Random.Range(0, deck.Count)];
        deck.Remove(drawnCard);

        return drawnCard;
    }

    public T Draw()
    {
        if (!CanDraw())
        {
            return default;
        }

        T drawnCard = deck[0];
        deck.Remove(drawnCard);

        return drawnCard;
    }

    public bool CanDraw()
    {
        if (deck.Count == 0)
        {
            Debug.Log("Deck is empty...");
            return false;
        }

        return true;
    }
    #endregion

    public void AddCardUnder(T card)
    {
        deck.Add(card);
    }

    //TODO: find a shuffle method to implement. 
    public void Shuffle()
    {

    }
}

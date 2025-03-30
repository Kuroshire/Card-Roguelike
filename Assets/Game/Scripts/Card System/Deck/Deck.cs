using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck<T>
{
    private readonly List<T> deck;

    public Deck(List<T> cards)
    {
        deck = new();
        cards.ForEach(card => AddCardUnder(card));
    }

    public T DrawRandom() {
        if(!CanDraw()) {
            return default;
        }

        T drawnCard = deck[Random.Range(0, deck.Count)];
        deck.Remove(drawnCard);
        
        return drawnCard;
    }

    public T Draw() {
        if(!CanDraw()) {
            return default;
        }

        T drawnCard = deck[0];
        deck.Remove(drawnCard);
        
        return drawnCard;
    }

    public void AddCardUnder(T card) {
        deck.Add(card);
    }

    public bool CanDraw() {
        if(deck.Count == 0) {
            Debug.Log("Deck is empty...");
            return false;
        }

        return true;
    }
}

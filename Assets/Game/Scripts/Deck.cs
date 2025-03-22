using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck
{
    private readonly List<Card> deck;

    public Deck(List<Card> cards)
    {
        deck = new();
        cards.ForEach(card => AddCardUnder(card));
    }

    public Card DrawRandom() {
        if(!CanDraw()) {
            return null;
        }

        Card drawnCard = deck[Random.Range(0, deck.Count)];
        deck.Remove(drawnCard);
        
        return drawnCard;
    }

    public Card Draw() {
        if(!CanDraw()) {
            return null;
        }

        Card drawnCard = deck[0];
        deck.Remove(drawnCard);
        
        return drawnCard;
    }

    public void AddCardUnder(Card card) {
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

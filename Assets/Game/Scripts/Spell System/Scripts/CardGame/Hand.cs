using System.Collections.Generic;
using Sirenix.Utilities.Editor;

public class Hand<T>
{
    private readonly List<T> hand = new();
    public int MaxHandSize { get; private set; }

    public int CardsInHandCount => hand.Count;

    public Hand(int maxHandSize = 7)
    {
        MaxHandSize = maxHandSize;
    }

    public List<T> GetCards()
    {
        return hand;
    }

    public bool IsHandFull()
    {
        return hand.Count >= MaxHandSize;
    }

    public bool AddCardToHand(T card)
    {
        if (!IsHandFull())
        {
            hand.Add(card);
            return true;
        }
        return false;
    }

    public bool RemoveCardFromhand(T card)
    {
        return hand.Remove(card);
    }

    public override string ToString()
    {
        string result = "";

        foreach (T card in hand)
        {
            result += card.ToString() + ", ";
        }

        return result[^2..];
    }
}

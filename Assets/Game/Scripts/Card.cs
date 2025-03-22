using UnityEngine;

public class Card {
    private readonly CardData cardData;

    public Card(CardData cardData) {
        this.cardData = cardData;
        Cost = cardData.Cost;
        Effect = cardData.Effect;
    }

    public Sprite Sprite { get => cardData.Sprite; }
    public string Title { get => cardData.name; }
    public int Cost { get; set; }
    public string Effect { get; set; }

    public void PerformEffect() {
        Debug.Log("Effect : " + Effect + "\nCost : " + Cost);
    }

    public override string ToString() {
        return "Name : " + Title + ", Cost : " + Cost + "Effect : " + Effect;
    }
}

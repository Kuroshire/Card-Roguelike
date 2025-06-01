using TMPro;
using UnityEngine;

public class ShowCardsLeftInDeck : MonoBehaviour
{
    private CardManager cardManager;
    [SerializeField] private TextMeshProUGUI cardText;

    public void Initialise(CardManager cardManager)
    {
        this.cardManager = cardManager;
        
        cardManager.OnDrawCard += UpdateCardsLeftDisplay;
        UpdateCardsLeftDisplay();
    }

    public void UpdateCardsLeftDisplay(Rune _ = null) {
        cardText.text = cardManager.CardsLeftInDeck.ToString();
    }
}

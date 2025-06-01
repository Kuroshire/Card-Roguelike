using UnityEngine;

public class SpellUIManager : MonoBehaviour
{
    
    [SerializeField] private CardManager cardManager;
    [SerializeField] private SpellManager spellManager;

    [SerializeField] private RuneRecipeShow runeRecipeShow;
    [SerializeField] private ShowCardsLeftInDeck cardsLeftInDeck;

    public void Initialise()
    {
        runeRecipeShow.Initialise(cardManager, spellManager);
        cardsLeftInDeck.Initialise(cardManager);
        // Debug.Log("spell UI initialised !");
    }
}

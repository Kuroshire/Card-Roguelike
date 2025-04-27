using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHandler : MonoBehaviour {
    [SerializeField] private HandManager handManager;
    [SerializeField] private RuneView runeView;
    [SerializeField] private HandCardsPositionHandler cardsPositionHandler;
    [SerializeField] private Transform cardSpawnPoint;
    [SerializeField] private int cardPerHand;

    private Deck<Rune> deck;

    public void CreateDeckRandom(int size) {
        List<Rune> deckOfRunes = new();
        RuneCollection collection = GameCollections.AllRunes;

        int amountOfRunes = collection.RuneList.Count;

        for(int i = 0; i < size; i++) {
            RuneData data = collection.RuneList[Random.Range(0, amountOfRunes)];
            Rune card = new(data);

            deckOfRunes.Add(card);
        }

        deck = new(deckOfRunes);
    }

    
    #region Draw Logic

    private bool isDrawing = false;

    public void DrawHand() {
        StartCoroutine(DrawHandCoroutine());
    }

    private IEnumerator DrawHandCoroutine() {
        if(isDrawing == true) {
            Debug.Log("already drawing...");
            yield break;
        }

        isDrawing = true;
        int numberOfDraws = cardPerHand - handManager.NumberOfCardInHand;
        for(int i = 0; i < numberOfDraws; i++) {
            DrawOne();
            yield return new WaitForSeconds(0.1f);
        }

        isDrawing = false;
    }

    public void DrawOne() {
        if(!handManager.CanDraw) {
            Debug.Log("Hand is full...");
            return;
        }

        //create card gameObject
        Rune drawnRune = deck.Draw();
        if(drawnRune == null) {
            return;
        }
        
        RuneView view = Instantiate(runeView, cardSpawnPoint.position, cardSpawnPoint.rotation);
        view.Setup(drawnRune, cardsPositionHandler.SplinePositionY);
        
        handManager.OnDrawCard(view);
    }

    #endregion
}

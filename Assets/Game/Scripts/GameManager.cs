using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager: MonoBehaviour {
    [SerializeField] private List<CardData> cardDatas;
    [SerializeField] private CardView cardView;
    [SerializeField] private HandManager handManager;
    [SerializeField] private Transform cardSpawnPoint;
    [SerializeField] private int cardPerHand;
    private Deck deck;

    void Start()
    {
        CreateDeck(20);
        
        Invoke(nameof(DrawHand), .5f);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            DrawHand();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            ToggleCardHovered();
        }
    }

    private void CreateDeck(int size) {
        List<Card> deckOfCards = new();

        for(int i = 0; i < size; i++) {
            CardData data = cardDatas[Random.Range(0, cardDatas.Count)];
            Card card = new(data);

            deckOfCards.Add(card);
        }

        deck = new(deckOfCards);
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
            DrawCard();
            yield return new WaitForSeconds(0.1f);
        }

        isDrawing = false;
    }

    public void DrawCard() {
        if(!handManager.CanDraw) {
            Debug.Log("Hand is full...");
            return;
        }

        //create card gameObject
        Card drawnCard = deck.Draw();
        if(drawnCard == null) {
            return;
        }
        
        CardView view = Instantiate(cardView, cardSpawnPoint.position, cardSpawnPoint.rotation);
        view.Setup(drawnCard, handManager.SplinePositionY);
        
        handManager.OnDrawCard(view);
    }

    #endregion

    public void UseSelectedCardList() {
        List<CardView> selectedCardList = handManager.GetSelectedCardList();

        selectedCardList.ForEach((card) => {
            card.Use();
        });
    }

    public void ToggleCardHovered() {
        CardView currentHover = MouseHoverDetection.CurrentHover;
        if(currentHover == null) {
            return;
        }

        if(handManager.HasCardInHand(currentHover)) {
            handManager.ToggleCardSelection(currentHover);
        }
    }
}
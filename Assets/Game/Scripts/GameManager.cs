using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager: MonoBehaviour {
    [SerializeField] private List<CardData> cardDatas;
    [SerializeField] private CardView cardView;
    [SerializeField] private HandManager handManager;
    [ShowInInspector] private Deck deck;
    [SerializeField] private Transform cardSpawnPoint;

    void Start()
    {
        List<Card> deckOfCards = new();

        for(int i = 0; i < 20; i++) {
            CardData data = cardDatas[Random.Range(0, cardDatas.Count)];
            Card card = new(data);

            deckOfCards.Add(card);
        }

        deck = new(deckOfCards);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            DrawCard();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            UseSelectedCard();
        }
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
        view.Setup(drawnCard);
        
        handManager.OnDrawCard(view);
    }

    public void UseSelectedCard() {
        if(handManager.SelectedCard != null) {
            handManager.SelectedCard.Use();
        }
    }
}
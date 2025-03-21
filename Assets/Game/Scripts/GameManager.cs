using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager: MonoBehaviour {
    [SerializeField] private List<CardData> cardDatas;
    [SerializeField] private CardView cardView;
    private List<Card> deck;

    [SerializeField] private HandManager handManager;
    [SerializeField] private Transform cardSpawnPoint;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            DrawCard();
        }
    }

    void Start()
    {
        deck = new();

        for(int i = 0; i < 20; i++) {
            CardData data = cardDatas[Random.Range(0, cardDatas.Count)];
            Card card = new(data);

            deck.Add(card);
        }
    }

    public void DrawCard() {
        if(!handManager.CanDraw) {
            Debug.Log("Hand is full...");
            return;
        }

        //create card gameObject
        Card drawnCard = deck[Random.Range(0, deck.Count)];
        deck.Remove(drawnCard);
        CardView view = Instantiate(cardView, cardSpawnPoint.position, cardSpawnPoint.rotation);
        view.Setup(drawnCard);
        
        handManager.OnDrawCard(view);
    }
}
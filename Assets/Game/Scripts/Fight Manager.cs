using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager: MonoBehaviour {
    [SerializeField] private RuneCollection collection;
    [SerializeField] private RuneView runeView;
    [SerializeField] private HandManager handManager;
    [SerializeField] private Transform cardSpawnPoint;
    [SerializeField] private int cardPerHand;

    [SerializeField] private SpellManager spellManager;
    private Deck<Rune> deck;

    public event Action<SpellData> OnSpellUse;

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
        List<Rune> deckOfRunes = new();
        int amountOfRunes = collection.RuneList.Count;

        for(int i = 0; i < size; i++) {
            RuneData data = collection.RuneList[UnityEngine.Random.Range(0, amountOfRunes)];
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
        view.Setup(drawnRune, handManager.SplinePositionY);
        
        handManager.OnDrawCard(view);
    }

    #endregion

    public void UseSelectedRuneList() {
        List<RuneView> selectedRuneList = handManager.GetSelectedRuneList();
        List<RuneElement> selectedRunes = handManager.GetElementsFromSelectedRunes();


        List<SpellData> foundSpells = spellManager.FindValidSpell(selectedRunes);
        
        selectedRuneList.ForEach((card) => {
            card.Use();
        });

        if(foundSpells.Count == 0) {
            //no matching spells...
            Debug.Log("no spell found");
            return;
        }

        OnSpellUse?.Invoke(foundSpells[0]);
    }

    public void ToggleCardHovered() {
        IHoverable currentHover = MouseHoverDetection.CurrentHover;
        if(currentHover == null) {
            return;
        }

        currentHover.OnClick();
    }
}
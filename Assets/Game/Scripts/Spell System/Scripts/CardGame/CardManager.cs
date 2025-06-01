using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // --- References ---
    [SerializeField] private RuneView runeViewPrefab;
    [SerializeField] private Transform handParent;

    [SerializeField] private HandCardsPositionHandler cardsPositionHandler;
    [SerializeField] private Transform cardSpawnPoint;

    // --- Attributes ---
    private Deck<Rune> deck;
    private Hand<RuneView> hand;
    public List<RuneView> CardsInHand => hand.GetCards();

    // --- GETTERS ---
    public int MaxHandSize => hand.MaxHandSize;
    public int CardsLeftInDeck => deck.CardsLeft;

    // --- Events ---
    public event Action OnHandSelectionChange;
    public event Action OnHandChanges;
    public event Action<Rune> OnDrawCard;
    public event Action<RuneView> OnRuneViewRemovedFromHand;

    public void Initialise(int deckSize, int maxHandSize)
    {
        deck = new(deckSize);
        hand = new(maxHandSize);

        cardsPositionHandler.Initialise();
    }

    #region RuneView Handling
    private RuneView CreateRuneViewCard(Rune rune)
    {
        RuneView newRuneView = Instantiate(runeViewPrefab, handParent);
        newRuneView.transform.position = (Vector2) Camera.main.ScreenToWorldPoint(cardSpawnPoint.position);

        newRuneView.Setup(rune, cardsPositionHandler.SplinePositionY, cardsPositionHandler.CardSelectedPositionY, this);
        newRuneView.SetSortingOrder(hand.CardsInHandCount);

        return newRuneView;
    }

    public void RemoveRuneViewFromHand(RuneView runeView)
    {
        // Debug.Log("Removing runeView from hand : " + runeView);
        bool wasInHand = hand.RemoveCardFromhand(runeView);
        if (wasInHand)
        {
            // Debug.Log("removed from hand !");
            runeView.OnSelectionChanged -= OnHandSelectionChange;
            runeView.OnSelectionChanged -= OnHandChanges;
            runeView.OnRuneUsed -= RemoveRuneViewFromHand;

            OnRuneViewRemovedFromHand?.Invoke(runeView);
        }

        Debug.Log(hand.ToString());
    }
    
    public void CallHandSelectionChange()
    {
        // Debug.Log("hand selction changes...");
        OnHandSelectionChange?.Invoke();
    }

    public void CallHandChange()
    {
        // Debug.Log("hand changes...");
        OnHandChanges?.Invoke();
    }
    

    #region Selection Lock Logic
    private void UnlockOnSpellUsed(SpellData _)
    {
        UnlockSelection();
    }

    public void LockSelection()
    {
        CardsInHand.ForEach((runeView) => runeView.Lock());
    }

    public void UnlockSelection()
    {
        CardsInHand.ForEach((runeView) => runeView.Unlock());
    }
    #endregion
    
    #region Selected Cards Accessors
    public List<RuneView> GetSelectedRuneList()
    {
        return CardsInHand.FindAll((runeView) => runeView.IsSelected);
    }

    public List<RuneElement> GetElementsFromSelectedRunes()
    {
        List<RuneView> selectedRunes = GetSelectedRuneList();
        return selectedRunes.ConvertAll((runeView) => runeView.RuneElement);
    }
    #endregion

    #endregion

    #region Draw Logic
    public bool DrawOne()
    {
        if (hand.IsHandFull())
        {
            return false;
        }

        Rune runeDrawn = deck.DrawOne();
        RuneView newRuneView = CreateRuneViewCard(runeDrawn);

        hand.AddCardToHand(newRuneView);

        OnDrawCard?.Invoke(runeDrawn);
        return true;
    }

    public void DrawMultiple(int amount)
    {
        StartCoroutine(DrawMultipleCoroutine(amount));
    }

    public void FillHand()
    {
        int numberOfCardsToDraw = hand.MaxHandSize;
        StartCoroutine(DrawMultipleCoroutine(numberOfCardsToDraw));
    }

    private bool isDrawing = false;
    private IEnumerator DrawMultipleCoroutine(int amount)
    {
        if (isDrawing == true)
        {
            // Debug.Log("already drawing...");
            yield break;
        }

        isDrawing = true;

        for (int i = 0; i < amount; i++)
        {
            if (DrawOne() == false)
            {
                // Debug.Log("Drew " + (i + 1) + " cards. Hand is full now.");
                // Debug.Log($"Hand : {hand.CardsInHandCount}, max size : {hand.MaxHandSize}");
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }

        isDrawing = false;
    }

    #endregion
}

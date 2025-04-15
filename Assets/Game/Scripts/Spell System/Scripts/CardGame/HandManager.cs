using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class HandManager : MonoBehaviour
{
    [SerializeField] private int maxHandSize;
    private readonly List<RuneView> hand = new();
    public List<RuneView> Hand => hand;
    public int MaxHandSize => maxHandSize;
    public int NumberOfCardInHand => hand.Count;
    public bool CanDraw => hand.Count < maxHandSize;

    public event Action OnHandSelectionChanges;
    public event Action OnHandChange;

    public List<RuneView> GetSelectedRuneList() {
        List<RuneView> selectedRuneList = new();
        hand.ForEach((rune) => {
            if(rune.IsSelected) {
                selectedRuneList.Add(rune);
            }
        });

        return selectedRuneList;
    }

    public List<RuneElement> GetElementsFromSelectedRunes() {
        List<RuneElement> selectedElementList = new();
        hand.ForEach((rune) => {
            if(rune.IsSelected) {
                selectedElementList.Add(rune.RuneElement);
            }
        });

        return selectedElementList;
    }

    public bool HasCardInHand(RuneView card) {
        if(card == null) {
            return false;
        }

        return hand.Contains(card);
    }

    public void OnDrawCard(RuneView newRune) {
        hand.Add(newRune);
        newRune.SetSortingOrder(hand.Count);
        newRune.OnRuneUsed += RemoveCardOnUse;
        newRune.OnSelectionChanged += HandleSelectionChanges;

        OnHandChange?.Invoke();
    }

    private void RemoveCardOnUse(RuneView card) {
        hand.Remove(card);
        card.OnRuneUsed -= RemoveCardOnUse;
        card.OnSelectionChanged -= HandleSelectionChanges;

        OnHandChange?.Invoke();
    }

    public void HandleSelectionChanges() {
        OnHandChange?.Invoke();

        OnHandSelectionChanges?.Invoke();
    }
}

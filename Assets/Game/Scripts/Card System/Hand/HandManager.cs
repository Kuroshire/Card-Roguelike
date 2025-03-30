using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class HandManager : MonoBehaviour
{
    [SerializeField] private int maxHandSize;
    [SerializeField] private SplineContainer splineContainer;
    public float SplinePositionY => splineContainer.EvaluatePosition(0).y;

    private readonly List<RuneView> hand = new();

    public int NumberOfCardInHand => hand.Count;

    [SerializeField] private float pushStrength = 0.05f;

    public bool CanDraw => hand.Count < maxHandSize;

    public event Action OnHandSelectionChanges;

    void Start()
    {
        MouseHoverDetection.Instance.OnHoverChange += UpdateCardPositionAndOrder;
    }

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

        UpdateCardPositionAndOrder();
    }

    private void RemoveCardOnUse(RuneView card) {
        hand.Remove(card);
        card.OnRuneUsed -= RemoveCardOnUse;
        card.OnSelectionChanged -= HandleSelectionChanges;

        UpdateCardPositionAndOrder();
    }

    public void HandleSelectionChanges() {
        UpdateCardPositionAndOrder();

        OnHandSelectionChanges?.Invoke();
    }

    #region Card Position Update

    private void UpdateCardPositionAndOrder() {
        UpdateCardOrder();
        UpdateCardPositions();
    }

    private void UpdateCardOrder() {
        for(int i = 0; i < hand.Count; i++) {
            int order = i;
            hand[i].SetSortingOrder(order);
        }
    }

    private void UpdateCardPositions() {
        if(hand.Count == 0) {
            return;
        }

        float cardSpacing = 1f/ maxHandSize;
        float firstCardPosition = 0.5f - (hand.Count - 1 ) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;

        int hoveredCardIndex = hand.FindIndex((card) => card == (RuneView) MouseHoverDetection.CurrentHover);

        for(int i = 0; i < hand.Count; i++) {
            float p = firstCardPosition + i * cardSpacing;

            // Apply "push" effect if there's a selected card
            if (hoveredCardIndex != -1)
            {
                float distanceFromSelected = Mathf.Abs(i - hoveredCardIndex);
                if (distanceFromSelected > 0) // Don't move the selected card itself
                {
                    float pushEffect = pushStrength / distanceFromSelected; // Less push for farther cards
                    p += (i < hoveredCardIndex) ? -pushEffect : pushEffect; // Push left or right
                }
            }

            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);

            hand[i].transform.DOMoveX(splinePosition.x, 0.25f);
            hand[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);
        }
    }

    #endregion
}

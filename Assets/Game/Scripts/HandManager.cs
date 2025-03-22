using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class HandManager : MonoBehaviour
{
    [SerializeField] private int maxHandSize;
    [SerializeField] private SplineContainer splineContainer;

    private readonly List<CardView> handCards = new();

    private CardView selectedCard = null;
    public CardView SelectedCard => selectedCard;

    [SerializeField] private float pushStrength = 0.05f;

    public bool CanDraw => handCards.Count < maxHandSize;

    public void SelectCard(CardView card) {
        if(selectedCard == card) {
            //doesn't change anything
            return;
        }

        if(selectedCard != null) {
            selectedCard.Unselect();
        }

        if(card != null) {
            card.Select();
        }

        selectedCard = card;
        UpdateCardPositionAndOrder();
    }

    public void OnDrawCard(CardView newCard) {
        handCards.Add(newCard);
        newCard.SetSortingOrder(handCards.Count);
        newCard.OnCardUsed += DestroyCardOnUse;

        UpdateCardPositionAndOrder();
    }

    private void DestroyCardOnUse(CardView card) {
        handCards.Remove(card);
        Destroy(card.gameObject);

        UpdateCardPositionAndOrder();
    }

    private void UpdateCardPositionAndOrder() {
        UpdateCardOrder();
        UpdateCardPositions();
    }

    private void UpdateCardOrder() {
        for(int i = 0; i < handCards.Count; i++) {
            handCards[i].SetSortingOrder(i);
        }

        if(selectedCard != null) {
            selectedCard.SetSortingOrder(maxHandSize + 1);
        }
    }

    private void UpdateCardPositions() {
        if(handCards.Count == 0) {
            return;
        }

        float cardSpacing = 1f/ maxHandSize;
        float firstCardPosition = 0.5f - (handCards.Count - 1 ) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;

        int selectedCardIndex = handCards.FindIndex((card) => card == selectedCard);

        for(int i = 0; i < handCards.Count; i++) {
            float p = firstCardPosition + i * cardSpacing;

            // Apply "push" effect if there's a selected card
            if (selectedCardIndex != -1)
            {
                float distanceFromSelected = Mathf.Abs(i - selectedCardIndex);
                if (distanceFromSelected > 0) // Don't move the selected card itself
                {
                    float pushEffect = pushStrength / distanceFromSelected; // Less push for farther cards
                    p += (i < selectedCardIndex) ? -pushEffect : pushEffect; // Push left or right
                }
            }

            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);

            handCards[i].transform.DOMove(splinePosition, 0.25f);
            handCards[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);
        }
    }


}

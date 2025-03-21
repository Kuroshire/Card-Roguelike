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

    public bool CanDraw => handCards.Count < maxHandSize;

    public void OnDrawCard(CardView newCard) {
        handCards.Add(newCard);
        newCard.SetSortingOrder(handCards.Count);
        newCard.OnCardUsed += DestroyCardOnUse;

        UpdateCardPositions();
    }

    private void DestroyCardOnUse(CardView card) {
        handCards.Remove(card);
        Destroy(card.gameObject);

        UpdateCardPositions();
    }

    private void UpdateCardPositions() {
        if(handCards.Count == 0) {
            return;
        }

        float cardSpacing = 1f/ maxHandSize;
        float firstCardPosition = 0.5f - (handCards.Count - 1 ) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;

        for(int i = 0; i < handCards.Count; i++) {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);

            handCards[i].transform.DOMove(splinePosition, 0.25f);
            handCards[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);
        }
    }


}

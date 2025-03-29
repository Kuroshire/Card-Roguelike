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

    private readonly List<CardView> handCards = new();

    public int NumberOfCardInHand => handCards.Count;

    [SerializeField] private float pushStrength = 0.05f;

    public bool CanDraw => handCards.Count < maxHandSize;

    void Start()
    {
        MouseHoverDetection.Instance.OnHoverChange += UpdateCardPositionAndOrder;
    }

    public List<CardView> GetSelectedCardList() {
        List<CardView> selectedCardList = new();
        handCards.ForEach((card) => {
            if(card.IsSelected) {
                selectedCardList.Add(card);
            }
        });

        return selectedCardList;
    }

    public bool HasCardInHand(CardView card) {
        if(card == null) {
            return false;
        }

        return handCards.Contains(card);
    }

    public void ToggleCardSelection(CardView card) {
        if(card == null) {
            UpdateCardPositionAndOrder();
            return;
        }

        if(!card.IsSelected) {
            card.Select();
        } else {
            card.Unselect();
        }

        UpdateCardPositionAndOrder();
    }

    public void OnDrawCard(CardView newCard) {
        handCards.Add(newCard);
        newCard.SetSortingOrder(handCards.Count);
        newCard.OnCardUsed += RemoveCardOnUse;

        UpdateCardPositionAndOrder();
    }

    private void RemoveCardOnUse(CardView card) {
        handCards.Remove(card);
        UpdateCardPositionAndOrder();
    }

    #region Card Position Update

    private void UpdateCardPositionAndOrder() {
        UpdateCardOrder();
        UpdateCardPositions();
    }

    private void UpdateCardOrder() {
        for(int i = 0; i < handCards.Count; i++) {
            int order = i;
            // if(handCards[i].IsSelected) {
            //     order += maxHandSize;
            // }
            handCards[i].SetSortingOrder(order);
        }
    }

    private void UpdateCardPositions() {
        if(handCards.Count == 0) {
            return;
        }

        float cardSpacing = 1f/ maxHandSize;
        float firstCardPosition = 0.5f - (handCards.Count - 1 ) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;

        int hoveredCardIndex = handCards.FindIndex((card) => card == MouseHoverDetection.CurrentHover);

        for(int i = 0; i < handCards.Count; i++) {
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

            handCards[i].transform.DOMoveX(splinePosition.x, 0.25f);
            handCards[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);
        }
    }

    #endregion
}

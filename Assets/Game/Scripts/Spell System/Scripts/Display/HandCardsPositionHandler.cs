using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class HandCardsPositionHandler: MonoBehaviour {

    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private Transform seletectedPosition;
    public float SplinePositionY => splineContainer.EvaluatePosition(0).y;
    public float CardSelectedPositionY => seletectedPosition.position.y;
    [SerializeField] private float pushStrength = 0.05f;
    [SerializeField] private CardManager cardManager;

    public int MaxHandSize => cardManager.MaxHandSize;
    public List<RuneView> Hand => cardManager.CardsInHand;


    public void Initialise()
    {
        MouseHoverDetection.Instance.OnHoverChange += UpdateCardPositionAndOrder;
        cardManager.OnHandChanges += UpdateCardPositionAndOrder;
        cardManager.OnDrawCard += UpdateHandPosition;
    }


    #region Card Position Update

    public void UpdateHandPosition<T>(T _)
    {
        UpdateCardPositionAndOrder();
    }

    private void UpdateCardPositionAndOrder()
    {
        UpdateCardOrder();
        UpdateCardPositions();
    }

    private void UpdateCardOrder() {
        for(int i = 0; i < Hand.Count; i++) {
            int order = i;
            Hand[i].SetSortingOrder(order);
        }
    }

    private void UpdateCardPositions() {
        if(Hand.Count == 0) {
            return;
        }

        float cardSpacing = 1f/ MaxHandSize;
        float firstCardPosition = 0.5f - (Hand.Count - 1 ) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;

        int hoveredCardIndex = Hand.FindIndex((card) => card == (RuneView) MouseHoverDetection.CurrentHover);

        for(int i = 0; i < Hand.Count; i++) {
            if (Hand[i] == null)
            {
                Debug.Log("this card was destroyed already: " + i);
            }
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

            Hand[i].transform.DOMoveX(splinePosition.x, 0.25f);
            Hand[i].SetPositionY();
            
            Hand[i].transform.DOLocalRotateQuaternion(rotation, 0.25f);
        }
    }

    #endregion

}
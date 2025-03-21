using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SpriteRenderer cardBackground, cardImage;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text cost;

    public Action<CardView> OnCardUsed;

    private Card card;

    public void Setup(Card card) {
        this.card = card;
        cardImage.sprite = card.Sprite;
        title.text = card.Title;
        cost.text = card.Cost.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        card.PerformEffect();
        OnCardUsed?.Invoke(this);
    }

    public void SetSortingOrder(int order) {
        cardBackground.sortingOrder = order;
        cardImage.sortingOrder = order;
    }
}

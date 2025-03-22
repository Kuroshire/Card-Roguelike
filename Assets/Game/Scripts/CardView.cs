using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cardBackground, cardImage;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text cost;

    public Action<CardView> OnCardUsed;

    private Card card;
    private int sortingOrder;

    public void Setup(Card card) {
        this.card = card;
        cardImage.sprite = card.Sprite;
        title.text = card.Title;
        Debug.Log(card);
        cost.text = card.Cost.ToString();
    }
    
    public void SetSortingOrder(int order) {
        sortingOrder = order;

        ApplySortingOrder(order);
    }

    private void ApplySortingOrder(int order) {
        cardBackground.sortingOrder = order;
        cardImage.sortingOrder = order;
        canvas.sortingOrder = order;
    }

    public void Use()
    {
        card.PerformEffect();
        OnCardUsed?.Invoke(this);
    }

    public void Select()
    {
        transform.DOScale(1.5f, 0.25f);
    }

    public void Unselect()
    {
        transform.DOScale(1, 0.25f);
    }
}

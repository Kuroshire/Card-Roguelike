using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cardBackground, cardImage;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text cost;

    [SerializeField] private Transform isSelectedPosition;
    [SerializeField] private float defaultPositionY;

    public Action<CardView> OnCardUsed;

    private Card card;
    private int sortingOrder;

    public bool IsSelected {get; private set;} = false;

    public void Setup(Card card, float defaultPositionY) {
        this.card = card;
        cardImage.sprite = card.Sprite;
        title.text = card.Title;
        cost.text = card.Cost.ToString();

        this.defaultPositionY = defaultPositionY;
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

        DestroyCardOnUse();
    }

    public void Select()
    {
        transform.DOMoveY(isSelectedPosition.position.y, 0.25f);
        IsSelected = true;
    }

    public void Unselect()
    {
        transform.DOMoveY(defaultPositionY, 0.25f);
        IsSelected = false;
    }

    private void DestroyCardOnUse() {
        Destroy(gameObject);
    }
}

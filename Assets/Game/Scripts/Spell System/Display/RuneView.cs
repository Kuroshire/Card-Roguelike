using System;
using DG.Tweening;
using UnityEngine;

public class RuneView : MonoBehaviour, IHoverable
{
    [SerializeField] private SpriteRenderer runeBackground, runeImage;

    [SerializeField] private Transform isSelectedPosition;
    [SerializeField] private float defaultPositionY;

    public Action<RuneView> OnRuneUsed;

    private Rune rune;
    public RuneElement RuneElement => rune.Element; 
    private int sortingOrder;

    public Action OnSelectionChanged;

    public bool IsSelected {get; private set;} = false;

    public void Setup(Rune rune, float defaultPositionY) {
        this.rune = rune;
        runeImage.sprite = rune.Sprite;

        this.defaultPositionY = defaultPositionY;
    }
    
    public void SetSortingOrder(int order) {
        sortingOrder = order;

        ApplySortingOrder(order);
    }

    private void ApplySortingOrder(int order) {
        runeBackground.sortingOrder = order;
        runeImage.sortingOrder = order;
    }

    public void Use()
    {
        rune.PerformEffect();
        OnRuneUsed?.Invoke(this);

        DestroyruneOnUse();
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

    private void DestroyruneOnUse() {
        Destroy(gameObject);
    }

    public void OnClick() {
        if(!IsSelected) {
            Select();
        } else {
            Unselect();
        }

        OnSelectionChanged?.Invoke();
    }
}

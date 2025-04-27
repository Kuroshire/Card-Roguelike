using System;
using DG.Tweening;
using UnityEngine;

public class RuneView : MonoBehaviour, IHoverable
{
    [SerializeField] private SpriteRenderer runeBackground, runeImage;
    [SerializeField] private Transform cardTransform;

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

        DestroyRuneOnUse();
    }

    public void Select()
    {
        cardTransform.DOMoveY(isSelectedPosition.position.y, 0.25f);
        IsSelected = true;
    }

    public void Unselect()
    {
        cardTransform.DOMoveY(defaultPositionY, 0.25f);
        IsSelected = false;
    }

    private void DestroyRuneOnUse() {
        Destroy(gameObject);
    }

    public void OnClick() {
        if(SpellSystemManager.HandManager.IsSelectionLocked == true) {
            Debug.Log("Cannot change current selection...");
            return;
        }

        if(!IsSelected) {
            Select();
        } else {
            Unselect();
        }

        OnSelectionChanged?.Invoke();
    }

    public void SetPositionY() {
        float position = IsSelected ? isSelectedPosition.position.y : defaultPositionY;
        cardTransform.DOMoveY(position, 0.25f);
    }
}

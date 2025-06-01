using System;
using DG.Tweening;
using UnityEngine;

public class RuneView : MonoBehaviour, IHoverable
{
    // --- Display Properties & References ---
    [Header("Display Properties & References")]
    [SerializeField] private SpriteRenderer runeBackground, runeImage;
    [SerializeField] private Transform cardTransform;
    [SerializeField] private float defaultPositionY, isSelectedPositionY;

    // --- Attributes ---
    private int sortingOrder;
    private Rune rune;
    public bool IsSelected { get; private set; } = false;
    public bool IsLocked { get; private set; } = false;

    // -- GETTERS ---
    public RuneElement RuneElement => rune.Element;

    // --- Events ---
    public Action OnSelectionChanged;
    public Action<RuneView> OnRuneUsed;


    public void Setup(Rune rune, float defaultPositionY, float isSelectedPositionY, CardManager cardManager)
    {
        this.rune = rune;
        runeImage.sprite = GameCollections.AllRunes.GetSpriteFromRuneElement(rune.Element);

        this.defaultPositionY = defaultPositionY;
        this.isSelectedPositionY = isSelectedPositionY;

        OnSelectionChanged += cardManager.CallHandSelectionChange;
        OnSelectionChanged += cardManager.CallHandChange;
        OnRuneUsed += cardManager.RemoveRuneViewFromHand;
    }

    public void SetSortingOrder(int order)
    {
        sortingOrder = order;

        ApplySortingOrder(order);
    }

    private void ApplySortingOrder(int order)
    {
        runeBackground.sortingOrder = order;
        runeImage.sortingOrder = order;
    }

    public void Use()
    {
        rune.PerformEffect();
        OnRuneUsed?.Invoke(this);

        DestroyRuneOnUse();
    }

    public void Lock()
    {
        IsLocked = true;
    }

    public void Unlock()
    {
        IsLocked = false;
    }

    public void Select()
    {
        cardTransform.DOMoveY(isSelectedPositionY, 0.25f);
        IsSelected = true;
    }

    public void Unselect()
    {
        cardTransform.DOMoveY(defaultPositionY, 0.25f);
        IsSelected = false;
    }

    private void DestroyRuneOnUse()
    {
        Destroy(gameObject);
    }

    public void OnClick()
    {
        if (IsLocked == true)
        {
            Debug.Log("Cannot change current selection...");
            return;
        }

        if (!IsSelected)
        {
            Select();
        }
        else
        {
            Unselect();
        }

        OnSelectionChanged?.Invoke();
    }

    public void SetPositionY()
    {
        float position = IsSelected ? isSelectedPositionY : defaultPositionY;
        cardTransform.DOMoveY(position, 0.25f);
    }

    public override string ToString()
    {
        return rune.ToString();
    }
}

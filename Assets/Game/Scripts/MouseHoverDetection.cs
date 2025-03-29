using System;
using System.Data;
using UnityEngine;

public class MouseHoverDetection: MonoBehaviour {
    #region Singleton

    public static MouseHoverDetection Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] private CardView currentHover;
    public static CardView CurrentHover => Instance.currentHover;

    public event Action OnHoverChange;

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        CardView newHover = null;
        if (hit.collider != null)
        {
            CardView card = hit.collider.GetComponentInParent<CardView>();
            if(card) {
                newHover = card;
            } else {
                // found something else than a cardview collider
            }
        } else {
            newHover = null;
        }

        if(currentHover != newHover) {
            // Debug.Log("changing hover !");
            currentHover = newHover;
            OnHoverChange?.Invoke();
        }
    }
}
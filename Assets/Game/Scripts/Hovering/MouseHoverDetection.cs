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

    [SerializeField] private IHoverable currentHover;
    public static IHoverable CurrentHover => Instance.currentHover;

    public event Action OnHoverChange;

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        IHoverable newHover = null;
        if (hit.collider != null)
        {
            IHoverable card = hit.collider.GetComponentInParent<IHoverable>();
            if(card != null) {
                newHover = card;
            } else {
                // found something else than a IHoverable collider
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

    public static void InteractWithHovered() {
        IHoverable currentHover = MouseHoverDetection.CurrentHover;
        if(currentHover == null) {
            return;
        }

        currentHover.OnClick();
    }
}

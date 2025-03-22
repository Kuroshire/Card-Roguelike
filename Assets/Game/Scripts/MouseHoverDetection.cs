using UnityEngine;

public class MouseHoverDetection: MonoBehaviour {
    [SerializeField] private HandManager handManager;

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            CardView card = hit.collider.GetComponentInParent<CardView>();
            if(card) {
                handManager.SelectCard(card);
            } else {
                // found something else than a cardview collider
            }
        } else {
            handManager.SelectCard(null);
        }
    }
}
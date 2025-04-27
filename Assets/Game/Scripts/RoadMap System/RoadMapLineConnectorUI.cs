using UnityEngine;
using UnityEngine.UI;

public class RoadMapLineConnectorUI : MonoBehaviour
{
    public RectTransform pointA;
    public RectTransform pointB;
    public Image lineImage;

    void Update()
    {
        if (pointA == null || pointB == null || lineImage == null)
            return;

        Vector3 worldPosA = pointA.position;
        Vector3 worldPosB = pointB.position;

        Vector3 direction = worldPosB - worldPosA;
        float distance = direction.magnitude;

        // Set position and size
        RectTransform lineRect = lineImage.rectTransform;
        lineRect.position = worldPosA;
        lineRect.sizeDelta = new Vector2(distance, lineRect.sizeDelta.y);

        // Set rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lineRect.rotation = Quaternion.Euler(0, 0, angle);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class TitleBar : MonoBehaviour, IDragHandler {
    [SerializeField] private RectTransform rootPanel;

    public void OnDrag(PointerEventData eventData) {
        Vector2 newPos = rootPanel.position;
        newPos.x += eventData.delta.x;
        newPos.y += eventData.delta.y;
        rootPanel.position = newPos;
    }
}

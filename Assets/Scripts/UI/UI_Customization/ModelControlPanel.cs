using UnityEngine;
using UnityEngine.EventSystems;

public class BackPanel : MonoBehaviour, IDragHandler {
    [SerializeField] private Transform model;

    public void OnDrag(PointerEventData eventData) {
        float dx = eventData.delta.x;
        Vector3 angle = model.eulerAngles;
        angle.y -= dx;
        model.rotation = Quaternion.Euler(angle);
    }
}

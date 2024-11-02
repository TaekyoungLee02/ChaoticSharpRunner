using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour, IPointerDownHandler, IDragHandler {
    public event Action<Color> ColorPreview;

    [SerializeField] RectTransform aim;

    private RectTransform rect;
    private Texture2D colorTexture;

    private Color color;
    private Vector2 initialAimPosition;

    public Color CurrentColor {
        get => color;
    }

    private void Awake() {
        rect = GetComponent<RectTransform>();
        colorTexture = GetComponent<Image>().mainTexture as Texture2D;
    }

    private void OnEnable() {
        initialAimPosition = aim.localPosition;
    }

    public void OnDrag(PointerEventData eventData) {
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, eventData.position, null, out localPos);

        localPos = new Vector2(
            Mathf.Clamp(localPos.x, 1f, rect.rect.width - 1f),
            Mathf.Clamp(localPos.y, 1f, rect.rect.height - 1f));

        aim.localPosition = localPos;

        float x = localPos.x / rect.rect.width;
        float y = localPos.y / rect.rect.height;

        int tx = Mathf.RoundToInt(x * colorTexture.width);
        int ty = Mathf.RoundToInt(y * colorTexture.height);

        color = colorTexture.GetPixel(tx, ty);

        ColorPreview?.Invoke(color);
    }

    public void OnPointerDown(PointerEventData eventData) {
        OnDrag(eventData);
    }

    public void ResetAimPosition() {
        aim.localPosition = initialAimPosition;
    }
}

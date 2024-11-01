using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour {
    private RectTransform rect;
    private Texture2D colorTexture;
    private Color color;

    public event Action<Color> ColorPreview;
    public event Action<Color> ColorSelect;
    public event Action Close;

    private void Start() {
        rect = GetComponent<RectTransform>();
        colorTexture = GetComponent<Image>().mainTexture as Texture2D;
    }

    private void Update() {
        Vector2 mousePos = Input.mousePosition;
        if (RectTransformUtility.RectangleContainsScreenPoint(rect, mousePos)) {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out Vector2 delta);

            float x = Mathf.Clamp(delta.x / rect.rect.width, 0f, 1f);
            float y = Mathf.Clamp(delta.y / rect.rect.height, 0f, 1f);

            int tx = Mathf.RoundToInt(x * colorTexture.width);
            int ty = Mathf.RoundToInt(y * colorTexture.height);

            color = colorTexture.GetPixel(tx, ty);

            ColorPreview?.Invoke(color);

            if (Input.GetMouseButtonDown(0)) {
                ColorSelect?.Invoke(color);
                Close?.Invoke();
                gameObject.SetActive(false);
            }
        } else {
            if (Input.GetMouseButtonDown(0)) {
                Close?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}

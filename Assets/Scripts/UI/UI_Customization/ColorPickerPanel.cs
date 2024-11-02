using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerPanel : MonoBehaviour {
    public event Action<Color> ColorSelect;
    public event Action Close;

    [SerializeField] private ColorPicker colorPicker;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private RectTransform rect;

    public ColorPicker ColorPicker {
        get => colorPicker;
    }

    private void Awake() {
        rect = GetComponent<RectTransform>();
        confirmButton.onClick.AddListener(Confirm);
        cancelButton.onClick.AddListener(Cancel);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (false == RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition)) {
                Cancel();
            }
        }
    }

    private void Cancel() {
        colorPicker.ResetAimPosition();
        Close?.Invoke();
        gameObject.SetActive(false);
    }

    private void Confirm() {
        ColorSelect?.Invoke(ColorPicker.CurrentColor);
        Close?.Invoke();
        gameObject.SetActive(false);
    }
}

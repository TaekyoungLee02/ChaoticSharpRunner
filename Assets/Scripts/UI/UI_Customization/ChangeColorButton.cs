using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorButton : MonoBehaviour {
    [SerializeField] ColorPicker colorPicker;
    [SerializeField] Material[] materialColors;

    private int count;
    private Color[] colors;
    private Button button;

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);

        count = materialColors.Length;
        colors = new Color[count];
    }

    private void OnButtonClicked() {
        for (int i = 0; i < count; i++) {
            colors[i] = materialColors[i].color;
        }

        colorPicker.gameObject.SetActive(true);
        colorPicker.ColorSelect += OnColorSelected;
        colorPicker.ColorPreview += OnColorPreview;
        colorPicker.Close += OnClose;
    }

    private void OnClose() {
        colorPicker.ColorSelect -= OnColorSelected;
        colorPicker.ColorPreview -= OnColorPreview;
        colorPicker.Close -= OnClose;

        for (int i = 0; i < count; i++) {
            materialColors[i].color = colors[i];
        }
    }

    private void OnColorPreview(Color color) {
        for (int i = 0; i < count; i++) {
            materialColors[i].color = color;
        }
    }

    private void OnColorSelected(Color color) {
        for (int i = 0; i < count; i++) {
            colors[i] = color;
        }
    }
}

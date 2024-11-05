using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorButton : MonoBehaviour {
    [SerializeField] ColorPickerPanel colorPickerPanel;
    [SerializeField] Material[] materialColors;

    private Button button;
    private Image image;

    private int count;
    private Color[] colors;

    private void Awake() {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        button.onClick.AddListener(OnButtonClicked);

        count = materialColors.Length;
        colors = new Color[count];

        if (count > 0) {
            image.color = materialColors[0].color;
        }
    }

    private void OnButtonClicked() {
        for (int i = 0; i < count; i++) {
            colors[i] = materialColors[i].color;
        }

        colorPickerPanel.gameObject.SetActive(true);
        colorPickerPanel.ColorSelect += OnColorSelected;
        colorPickerPanel.ColorPicker.ColorPreview += OnColorPreview;
        colorPickerPanel.Close += OnClose;
    }

    private void OnClose() {
        colorPickerPanel.ColorSelect -= OnColorSelected;
        colorPickerPanel.ColorPicker.ColorPreview -= OnColorPreview;
        colorPickerPanel.Close -= OnClose;

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
        image.color = color;
        for (int i = 0; i < count; i++) {
            colors[i] = color;
        }
    }
}

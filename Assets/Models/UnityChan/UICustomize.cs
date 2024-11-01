using System;
using UnityEngine;
using UnityEngine.UI;

public class UICustomize : MonoBehaviour {
    [SerializeField] CustomizationDataSO custom;
    [SerializeField] Button eyeColorWhiteButton;
    [SerializeField] Button eyeColorRedButton;
    [SerializeField] Button eyeColorBlueButton;

    private void Awake() {
        eyeColorWhiteButton.onClick.AddListener(OnEyeColorWhiteButtonClicked);
        eyeColorRedButton.onClick.AddListener(OnEyeColorRedButtonClicked);
        eyeColorBlueButton.onClick.AddListener(OnEyeColorBlueButtonClicked);
    }

    private void OnEyeColorWhiteButtonClicked() {
        custom.leftEyeMat.color = Color.white;
        custom.rightEyeMat.color = Color.white;
    }

    private void OnEyeColorBlueButtonClicked() {
        custom.leftEyeMat.color = Color.blue;
        custom.rightEyeMat.color = Color.blue;
    }

    private void OnEyeColorRedButtonClicked() {
        custom.leftEyeMat.color = Color.red;
        custom.rightEyeMat.color = Color.red;
    }
}

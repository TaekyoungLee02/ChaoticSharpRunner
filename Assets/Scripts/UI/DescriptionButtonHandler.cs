using System;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionButtonHandler : MonoBehaviour
{
    public Button descriptionButton;

    public void OnClickDescriptionButton()
    {
        GameManager.Instance.isPaused = false;
        Time.timeScale = 1;
        GameManager.Instance.OnSpeedStart?.Invoke();
    }

    void Start()
    {
        descriptionButton.onClick.AddListener(OnClickDescriptionButton);
    }
}
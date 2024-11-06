using UnityEngine;
using UnityEngine.UI;

public class PauseButtonHandler : MonoBehaviour
{
    public Button pauseButton;

    public void OnClickPauseButton()
    {
        GameManager.Instance.TogglePause();
    }

    void Start()
    {
        pauseButton.onClick.AddListener(OnClickPauseButton);
    }
}
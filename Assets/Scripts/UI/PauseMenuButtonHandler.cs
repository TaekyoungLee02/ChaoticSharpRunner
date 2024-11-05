using UnityEngine;
using UnityEngine.UI;

public class PauseMenuButtonHandler : MonoBehaviour
{
    public Button closeMenuButton;
    public Button restartGameButton;
    public Button goToMainMenuButton;

    public void OnClickCloseMenuButton()
    {
        GameManager.Instance.ResumeGame();
    }

    public void OnClickRestartGameButton()
    {
        GameManager.Instance.RestartGame();
    }

    public void OnClickGoToMainMenuButton()
    {
        GameManager.Instance.GoToMainMenu();
    }

    void Start()
    {
        closeMenuButton.onClick.AddListener(OnClickCloseMenuButton);
        restartGameButton.onClick.AddListener(OnClickRestartGameButton);
        goToMainMenuButton.onClick.AddListener(OnClickGoToMainMenuButton);
    }
}
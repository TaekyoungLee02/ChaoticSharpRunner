using UnityEngine;
using UnityEngine.UI;

public class PauseMenuButtonHandler : MonoBehaviour
{
    public Button closeMenuButton;
    public Button restartGameButton;
    public Button goToTitleSceneButton;

    public void OnClickCloseMenuButton()
    {
        GameManager.Instance.ResumeGame();
    }

    public void OnClickRestartGameButton()
    {
        GameManager.Instance.RestartGame();
    }

    public void OnClickGoToTitleSceneButton()
    {
        GameManager.Instance.GoToTitleScene();
    }

    void Start()
    {
        closeMenuButton.onClick.AddListener(OnClickCloseMenuButton);
        restartGameButton.onClick.AddListener(OnClickRestartGameButton);
        goToTitleSceneButton.onClick.AddListener(OnClickGoToTitleSceneButton);
    }
}
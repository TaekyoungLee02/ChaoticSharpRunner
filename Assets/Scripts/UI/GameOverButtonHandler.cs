using UnityEngine;
using UnityEngine.UI;

public class GameOverButtonHandler : MonoBehaviour
{
    public Button gameOverConfirmButton;
    public Button gameOverRefuseButton;

    public void OnClickgameOverConfirmButton()
    {
        GameManager.Instance.RestartGame();
    }

    public void OnClickGameOverRefuseButton()
    {
        GameManager.Instance.GoToTitleScene();
    }

    void Start()
    {
        gameOverConfirmButton.onClick.AddListener(OnClickgameOverConfirmButton);
        gameOverRefuseButton.onClick.AddListener(OnClickGameOverRefuseButton);
    }
}
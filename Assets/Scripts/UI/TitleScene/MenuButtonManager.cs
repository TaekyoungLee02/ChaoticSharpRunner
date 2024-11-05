using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private string[] sceneNames;
    [SerializeField] private Button initializeScoreButton;
    [SerializeField] private GameObject scoreResetConfirmationPanel; // 확인 패널 참조

    private Button selectedButton;

    private void Start()
    {
        DeselectAllButtons();

        initializeScoreButton.onClick.AddListener(ShowScoreResetConfirmationPanel);

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => HandleButtonClick(buttons[index], sceneNames[index]));
        }

        // 패널 초기화 상태
        scoreResetConfirmationPanel.SetActive(false);
    }

    // 초기화 확인 패널을 표시
    private void ShowScoreResetConfirmationPanel()
    {
        scoreResetConfirmationPanel.SetActive(true);
    }

    // 초기화 확인 패널에서 '예' 버튼을 클릭했을 때 실행
    public void OnConfirmResetScore()
    {
        ScoreManager.Instance.ResetHighScore();
        scoreResetConfirmationPanel.SetActive(false); // 패널 숨기기
    }

    // 초기화 확인 패널에서 '아니오' 버튼을 클릭했을 때 실행
    public void OnCancelResetScore()
    {
        scoreResetConfirmationPanel.SetActive(false); // 패널 숨기기
    }

    private void HandleButtonClick(Button button, string sceneName)
    {
        if (selectedButton == button)
        {
            LoadScene(sceneName);
        }
        else
        {
            DeselectAllButtons();
            HighlightButton(button);
            selectedButton = button;
        }
    }

    private void HighlightButton(Button button)
    {
        var colors = button.colors;
        colors.normalColor = new Color(1f, 0.8f, 0.2f, 1f);
        button.colors = colors;

        button.transform.localScale = new Vector3(1.1f, 1.1f, 1f);

        Outline outline = button.GetComponent<Outline>();
        if (outline != null)
        {
            outline.effectColor = new Color(1f, 0.8f, 0.2f, 1f);
            outline.effectDistance = new Vector2(5, 5);
        }
    }

    private void DeselectAllButtons()
    {
        foreach (var button in buttons)
        {
            ResetButtonColor(button);
        }
        selectedButton = null;
    }

    private void ResetButtonColor(Button button)
    {
        var colors = button.colors;
        colors.normalColor = Color.white;
        button.colors = colors;

        button.transform.localScale = Vector3.one;

        Outline outline = button.GetComponent<Outline>();
        if (outline != null)
        {
            outline.effectColor = new Color(0, 0, 0, 0);
            outline.effectDistance = new Vector2(0, 0);
        }
    }

    private void LoadScene(string sceneName)
    {
        if (sceneName == "Exit")
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
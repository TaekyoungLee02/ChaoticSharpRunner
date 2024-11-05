using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private string[] sceneNames;

    private Button selectedButton;

    private void Start()
    {
        DeselectAllButtons();

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => HandleButtonClick(buttons[index], sceneNames[index]));
        }
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
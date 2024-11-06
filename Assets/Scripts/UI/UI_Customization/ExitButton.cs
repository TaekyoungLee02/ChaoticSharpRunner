using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour {
    Button button;

    private void Awake () {
        button = GetComponent<Button>();
    }

    private void Start () {
        button.onClick.AddListener(ButtonClickHandler);
    }

    private void ButtonClickHandler () {
        SceneManager.LoadScene("TitleScene");
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject titleUIPrefab;
    [SerializeField] private GameObject inGameUIPrefab;
    [SerializeField] private GameObject gameOverPanelPrefab;
    [SerializeField] private GameObject pausePanelPrefab;

    private GameObject titleUIInstance;
    private GameObject inGameUIInstance;
    private GameObject gameOverPanelInstance;
    private GameObject pausePanelInstance;

    private GameObject canvasObject;
    private GameObject eventSystemObject;
        
    private void Start()
    {
        GameManager.Instance.OnTitleScreen += ShowTitleUI;
        GameManager.Instance.OnGameStart += ShowInGameUI;
        GameManager.Instance.OnGameOver += ShowGameOverPanel;
        GameManager.Instance.OnTogglePause += TogglePausePanel;
        GameManager.Instance.OnGoToTitleScene += HandleGoToTitleScene;
        GameManager.Instance.OnResumeGame += HidePausePanel;
        GameManager.Instance.OnGameRestart += HidePausePanel;
    }

    private void CreateCanvasAndEventSystem()
    {
        if (canvasObject == null)
        {
            canvasObject = new GameObject("Canvas");
            var canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f;

            canvasObject.AddComponent<GraphicRaycaster>();
        }

        if (eventSystemObject == null)
        {
            eventSystemObject = new GameObject("EventSystem");
            eventSystemObject.AddComponent<EventSystem>();
            eventSystemObject.AddComponent<StandaloneInputModule>();
        }
    }

    private void ShowTitleUI()
    {
        DestroyAllUIInstances();
        titleUIInstance = Instantiate(titleUIPrefab, canvasObject.transform);
    }

    private void ShowInGameUI()
    {
        DestroyAllUIInstances();
        inGameUIInstance = Instantiate(inGameUIPrefab, canvasObject.transform);
    }


    private void ShowGameOverPanel()
    {
        if (gameOverPanelInstance == null)
        {
            gameOverPanelInstance = Instantiate(gameOverPanelPrefab, canvasObject.transform);
        }
        gameOverPanelInstance.SetActive(true);
    }

    private void TogglePausePanel(bool isPaused)
    {
        if (pausePanelInstance == null)
        {
            pausePanelInstance = Instantiate(pausePanelPrefab, canvasObject.transform);
        }
        pausePanelInstance.SetActive(isPaused);
    }

    private void HidePausePanel()
    {
        if (pausePanelInstance != null)
        {
            pausePanelInstance.SetActive(false);
        }

        if (gameOverPanelInstance != null)
        {
            gameOverPanelInstance.SetActive(false);
        }
    }

    private void HandleGoToTitleScene()
    {
        DestroyAllUIInstances();
        ShowTitleUI();
    }

    private void DestroyAllUIInstances()
    {
        if (titleUIInstance != null) Destroy(titleUIInstance);
        if (inGameUIInstance != null) Destroy(inGameUIInstance);
        if (gameOverPanelInstance != null) Destroy(gameOverPanelInstance);
        if (pausePanelInstance != null) Destroy(pausePanelInstance);

        if (canvasObject == null || eventSystemObject == null)
        {
            CreateCanvasAndEventSystem();
        }
    }
}
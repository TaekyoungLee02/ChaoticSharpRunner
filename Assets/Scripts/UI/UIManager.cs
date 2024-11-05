using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject[] uiRootPrefabs;
    [SerializeField] private GameObject[] pausePanelPrefab;
    private GameObject uiRootInstance;
    private GameObject pausePanelInstance;

    private void Start()
    {
        SceneManager.sceneLoaded += SceneLoad;

        // ù ��° ���� TitleScene�� ��� Ÿ��Ʋ UI�� �ٷ� �ʱ�ȭ
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            InitializeUI(0); // Title UI�� �迭�� ù ��° ���������� ����
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoad;
    }

    void SceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "InGameScene")
        {
            InitializeUI(1); // InGame UI�� �迭�� �� ��° ���������� ����
        }
        else if (scene.name == "TitleScene")
        {
            InitializeUI(0); // Title UI�� �迭�� ù ��° ���������� ����
        }
    }

    public void InitializeUI(int prefabIndex)
    {
        if (uiRootInstance == null && uiRootPrefabs != null && prefabIndex >= 0 && prefabIndex < uiRootPrefabs.Length)
        {
            // �׻� ���ο� EventSystem ����
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();

            // �׻� ���ο� Canvas ����
            GameObject canvasObject = new GameObject("Canvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // CanvasScaler ����
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f;

            canvasObject.AddComponent<GraphicRaycaster>();

            // ���õ� UIRoot �������� ĵ���� ������ ����
            uiRootInstance = Instantiate(uiRootPrefabs[prefabIndex], canvas.transform);

            if (pausePanelPrefab[prefabIndex] != null)
            {
                pausePanelInstance = Instantiate(pausePanelPrefab[prefabIndex], canvas.transform);
                pausePanelInstance.SetActive(false);
            }
        }

        // UI ��� �ʱ�ȭ
        var condition = uiRootInstance?.GetComponentInChildren<UICondition>();
        condition?.Initialize();
    }

    public void TogglePauseMenu(bool show)
    {
        if (pausePanelInstance != null)
        {
            pausePanelInstance.SetActive(show);
        }
    }

    public void ResetUI()
    {
        if (uiRootInstance != null)
        {
            var condition = uiRootInstance.GetComponentInChildren<UICondition>();
            condition?.ResetUI();
        }
    }
}
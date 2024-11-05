using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject[] uiRootPrefabs; // ���� UIRoot ������ ����
    private GameObject uiRootInstance;

    private void Start()
    {
        SceneManager.sceneLoaded += SceneLoad;

        // ù ��° ���� TitleScene�� ��� Ÿ��Ʋ UI�� �ٷ� �ʱ�ȭ
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            InitializeUI(0); // Title UI�� �迭�� ù ��° ���������� ����
        }
    }

    void SceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "InGameScene") // �ΰ��� ������ �ʿ��� UI ����
        {
            InitializeUI(1); // InGame UI�� �迭�� �� ��° ���������� ����
        }
        else if (scene.name == "TitleScene") // Ÿ��Ʋ ������ ���ƿ� ��� Ÿ��Ʋ UI ����
        {
            InitializeUI(0); // Title UI�� �迭�� ù ��° ���������� ����
        }
    }

    public void InitializeUI(int prefabIndex)
    {
        if (uiRootInstance == null && uiRootPrefabs != null && prefabIndex >= 0 && prefabIndex < uiRootPrefabs.Length)
        {
            // EventSystem ����
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();

            // Canvas ����
            GameObject canvasObject = new GameObject("Canvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // CanvasScaler ����
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080); // �⺻ �ػ� ����
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f; // ȭ�� ������ �°� ����

            canvasObject.AddComponent<GraphicRaycaster>();

            // ���õ� UIRoot �������� ĵ���� ������ ����
            uiRootInstance = Instantiate(uiRootPrefabs[prefabIndex], canvas.transform);
        }

        // UI ��� �ʱ�ȭ
        var condition = uiRootInstance?.GetComponentInChildren<UICondition>();
        condition?.Initialize();
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

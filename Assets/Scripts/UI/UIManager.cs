using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject[] uiRootPrefabs; // 여러 UIRoot 프리팹 참조
    private GameObject uiRootInstance;

    private void Start()
    {
        SceneManager.sceneLoaded += SceneLoad;

        // 첫 번째 씬이 TitleScene인 경우 타이틀 UI를 바로 초기화
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            InitializeUI(0); // Title UI를 배열의 첫 번째 프리팹으로 가정
        }
    }

    void SceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "InGameScene") // 인게임 씬에서 필요한 UI 생성
        {
            InitializeUI(1); // InGame UI를 배열의 두 번째 프리팹으로 가정
        }
        else if (scene.name == "TitleScene") // 타이틀 씬으로 돌아올 경우 타이틀 UI 생성
        {
            InitializeUI(0); // Title UI를 배열의 첫 번째 프리팹으로 가정
        }
    }

    public void InitializeUI(int prefabIndex)
    {
        if (uiRootInstance == null && uiRootPrefabs != null && prefabIndex >= 0 && prefabIndex < uiRootPrefabs.Length)
        {
            // EventSystem 생성
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();

            // Canvas 생성
            GameObject canvasObject = new GameObject("Canvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // CanvasScaler 설정
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080); // 기본 해상도 설정
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f; // 화면 비율에 맞게 조정

            canvasObject.AddComponent<GraphicRaycaster>();

            // 선택된 UIRoot 프리팹을 캔버스 하위에 생성
            uiRootInstance = Instantiate(uiRootPrefabs[prefabIndex], canvas.transform);
        }

        // UI 요소 초기화
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

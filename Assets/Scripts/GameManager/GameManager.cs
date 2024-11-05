using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;
    public Player player;
    public event Action OnGameReset;
    public event Action OnGameOver;
    private bool isPaused = false;

    void Start()
    {
        SceneManager.sceneLoaded += SceneLoad;
    }

    void SceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "InGameScene")
        {
            InstantiatePlayer();
            InitializeGame();
        }
    }

    private void InstantiatePlayer()
    {
        if (playerPrefab != null && player == null)
        {
            GameObject playerObject = Instantiate(playerPrefab);
            player = playerObject.GetComponent<Player>();

            if (player == null)
            {
                Debug.LogError("Player 프리팹에 Player 컴포넌트가 없습니다.");
            }
            else
            {
                player.stats.OnPlayerDeath += EndGame;
                Debug.Log("Player가 성공적으로 생성되었습니다.");
            }
        }
        else if (player == null)
        {
            Debug.LogError("Player 프리팹이 할당되지 않았습니다.");
        }
    }

    private void InitializeGame()
    {
        if (player != null)
        {
            player.InitializePlayer();
            // 맵 초기화
            // 아이템 효과 초기화
            ScoreManager.Instance.InitializeScore();
        }
        else
        {
            Debug.LogError("Player가 초기화되지 않았습니다.");
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        UIManager.Instance.TogglePauseMenu(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        UIManager.Instance.TogglePauseMenu(false);
    }

    public void ResetGame()
    {
        player.ResetPlayer();
        OnGameReset?.Invoke();
    }

    public void EndGame()
    {
        OnGameOver?.Invoke();
        if (player != null && player.stats != null)
        {
            player.stats.OnPlayerDeath -= EndGame;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        UIManager.Instance.TogglePauseMenu(false);
        ScoreManager.Instance.InitializeScore();
        player.InitializePlayer();
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");
    }

    public void GameOver()
    {

    }
}
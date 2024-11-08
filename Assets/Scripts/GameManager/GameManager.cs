using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;
    public Player player;

    public event Action OnTitleScreen;
    public event Action OnGameStart;
    public event Action OnGameOver;
    public event Action OnGameReset;
    public event Action OnGameRestart;
    public event Action<bool> OnTogglePause;
    public event Action OnGoToTitleScene;
    public event Action OnResumeGame;
    public Action OnSpeedStart;

    public bool isPaused = false;
    private bool isGameOver = false;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            ShowTitleScreen();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "InGameScene")
        {
            StartGame();
            InstantiatePlayer();
        }
        else if (scene.name == "TitleScene")
        {
            ShowTitleScreen();
        }
    }

    private void InstantiatePlayer()
    {
        if (playerPrefab != null && player == null)
        {
            GameObject playerObject = Instantiate(playerPrefab);
            player = playerObject.GetComponent<Player>();
            player.InitializePlayer();
            player.stats.OnPlayerDeath += GameOver;
        }
    }

    public void ShowTitleScreen()
    {
        OnTitleScreen?.Invoke();
        if (!AudioManager.Instance.GetComponent<AudioSource>().isPlaying)
        {
            AudioManager.Instance.PlayBGMClip(AudioClipName.Bgm_CasualGame03, 0.3f);
        };
    }

    public void StartGame()
    {
        OnGameStart?.Invoke();
        isPaused = true;
        Time.timeScale = 1;
        isGameOver = false;
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        OnGameOver?.Invoke();
    }

    public void ResetGame()
    {
        player?.ResetPlayer();
        OnGameReset?.Invoke();
    }

    public void RestartGame()
    {
        ResumeState();
        player?.InitializePlayer();
        isGameOver = false;
        OnGameRestart?.Invoke();
        ScoreManager.Instance.InitializeScore();
    }

    public void TogglePause()
    {
        if (isGameOver) return;
        SetPauseState(!isPaused);
    }

    public void GoToTitleScene()
    {
        OnGoToTitleScene?.Invoke();
        ScoreManager.Instance.InitializeScore();
        SceneManager.LoadScene("TitleScene");
    }

    public void ResumeGame()
    {
        ResumeState();
        OnResumeGame?.Invoke();
    }

    private void ResumeState()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    private void SetPauseState(bool pause)
    {
        isPaused = pause;
        Time.timeScale = isPaused ? 0 : 1;
        OnTogglePause?.Invoke(isPaused);
    }
}
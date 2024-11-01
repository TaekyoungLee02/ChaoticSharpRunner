using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;

    public event Action OnGameReset;
    public event Action OnGameOver;

    void Start()
    {
        InitializeGame();
        player.stats.OnPlayerDeath += EndGame;
    }

    private void InitializeGame()
    {
        player.InitializePlayer();
        //UI
        //��
        //������ȿ��
    }

    public void ResetGame()
    {
        player.ResetPlayer();
        OnGameReset?.Invoke();
    }

    public void EndGame()
    {
        OnGameOver?.Invoke();
        //UIManager.Instance.ShowGameOverUI();
    }

    public void RestartGame()
    {
        InitializeGame();
    }
}
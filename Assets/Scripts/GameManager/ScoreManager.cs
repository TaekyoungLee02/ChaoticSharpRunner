using System;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private int currentScore = 0;
    private int highScore = 0;

    public event Action<int> OnScoreChanged;
    public event Action<int> OnHighScoreChanged;

    void Start()
    {
        LoadHighScore();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        OnHighScoreChanged?.Invoke(highScore);
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        OnScoreChanged?.Invoke(currentScore);

        if (currentScore > highScore)
        {
            highScore = currentScore;
            OnHighScoreChanged?.Invoke(highScore);
            SaveHighScore();
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void ResetHighScore()
    {
        highScore = 0;
        SaveHighScore();
        OnHighScoreChanged?.Invoke(highScore);
    }

    public void InitializeScore()
    {
        currentScore = 0;
        OnScoreChanged?.Invoke(currentScore);
    }
}
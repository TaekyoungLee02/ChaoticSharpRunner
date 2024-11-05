using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScoreText;
        ScoreManager.Instance.OnHighScoreChanged += UpdateHighScoreText;

        // 초기 점수 표시
        UpdateScoreText(ScoreManager.Instance.GetCurrentScore());
        UpdateHighScoreText(ScoreManager.Instance.GetHighScore());
    }
    
    private void UpdateScoreText(int score)
    {
        scoreText.text = $"{score}";
    }

    private void UpdateHighScoreText(int highScore)
    {
        highScoreText.text = $"{highScore}";
    }
}

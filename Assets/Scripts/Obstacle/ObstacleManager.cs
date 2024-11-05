using System;
using UnityEngine;

public class ObstacleManager : Singleton<ObstacleManager>
{
    private int passedObstacleCount;
    public int PassedObstacleCount { get { return passedObstacleCount; } }

    private void Start()
    {
        passedObstacleCount = PlayerPrefs.GetInt("ObstacleScore", 0);
    }

    public void ObstaclePassed()
    {
        passedObstacleCount++;
        AchievementManager.Instance.CheckAchievement(AchievementType.OBSTACLE, passedObstacleCount);
        PlayerPrefs.SetInt("ObstacleScore", passedObstacleCount);
        PlayerPrefs.Save();
    }
}

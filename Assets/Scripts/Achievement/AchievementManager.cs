using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : Singleton<AchievementManager>
{
    private List<AchievementSO> achievements;

    public List<AchievementSO> Achievements { get { return achievements; } }

    public override void Awake()
    {
        base.Awake();

        achievements = new List<AchievementSO>();

        var so = Resources.LoadAll("Achievements/");
        foreach (var item in so)
        {
            achievements.Add(item as AchievementSO);
        }
    }

    private void Start()
    {
        AchievementInit();
    }

    public void AchievementInit()
    {
        CheckAchievement(AchievementType.SCORE, ScoreManager.Instance.GetHighScore());
        CheckAchievement(AchievementType.ITEM, ItemManager.Instance.UsedItemCount);
        CheckAchievement(AchievementType.OBSTACLE, ObstacleManager.Instance.PassedObstacleCount);
    }

    public void CheckAchievement(AchievementType type, int value)
    {
        for(int i = 0; i < achievements.Count; i++)
        {
            if (achievements[i].itemType == type && achievements[i].achievementCondition <= value)
            {
                achievements[i].completed = true;
            }
        }
    }
}

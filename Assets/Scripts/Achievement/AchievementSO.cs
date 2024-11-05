using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "Achievement")]
public class AchievementSO : ScriptableObject
{
    [Header("Basic Information")]
    public string achievementName;
    public string achievementDescription;
    public AchievementType itemType;

    [Header("Condition")]
    public int achievementCondition;
    public bool completed;
}

public enum AchievementType
{
    SCORE,
    ITEM,
    OBSTACLE
}
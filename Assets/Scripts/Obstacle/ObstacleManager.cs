using UnityEngine;

public class ObstacleManager : Singleton<ObstacleManager>
{
    private int passedObstacleCount;
    public int PassedObstacleCount { get { return passedObstacleCount; } }

    private readonly string[] OBSTACLE_NAMES = { "Barrel", "Barrier", "BlockDoubleJump", "Slide" };

    private void Start()
    {
        passedObstacleCount = PlayerPrefs.GetInt("ObstacleScore", 0);   
    }

    public void SpawnObstacleInMap(MapScroller map)
    {
        foreach (var obsT in map.trapSpawnPosition)
        {
            int rand = Random.Range(0, OBSTACLE_NAMES.Length);

            var obstacle = ObjectPool.Instance.SpawnFromPool(OBSTACLE_NAMES[rand]);
            map.mapAttachedObjects.Add(obstacle.transform);

            obstacle.transform.position = obsT.position;
            obstacle.transform.SetParent(obsT);
        }
    }

    public void ObstaclePassed()
    {
        passedObstacleCount++;
        AchievementManager.Instance.CheckAchievement(AchievementType.OBSTACLE, passedObstacleCount);
        PlayerPrefs.SetInt("ObstacleScore", passedObstacleCount);
        PlayerPrefs.Save();
    }
}

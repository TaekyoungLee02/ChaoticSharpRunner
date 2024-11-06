using UnityEngine;

public class ObstacleManager : Singleton<ObstacleManager>
{
    private int passedObstacleCount;
    public int PassedObstacleCount { get { return passedObstacleCount; } }

    private readonly string[] OBSTACLE_NAMES = { "Barrel", "Barrier", "BlockDoubleJump", "Slide" };

    private ObjectPool pool;

    private void Start()
    {
        passedObstacleCount = PlayerPrefs.GetInt("ObstacleScore", 0);
        pool = ObjectPool.Instance;
    }

    public void SpawnObstacleInMap(MapScroller map)
    {
        foreach (var obsT in map.trapSpawnPosition)
        {
            int rand = Random.Range(0, OBSTACLE_NAMES.Length);

            var obstacle = pool.SpawnFromPool(OBSTACLE_NAMES[rand]);
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

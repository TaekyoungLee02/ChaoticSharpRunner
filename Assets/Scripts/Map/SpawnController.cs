using UnityEngine;

public enum ObjectType
{
    Item,
    Obstacle,
}

public enum ObstacleType
{

}

public class SpawnController : MonoBehaviour
{
    public string[] mapArray = { "CoinMap", "BaseMap", "errerMap" };

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            GetMapObject(ObjectPool.Instance).transform.position = new Vector3(0, 0, i * 30 + 1);
        }
    }

    public GameObject GetMapObject(ObjectPool inPool)
    {
        int randomMap = Random.Range(0, mapArray.Length);
        GameObject outNewMap = inPool.SpawnFromPool(mapArray[randomMap]);

        return outNewMap;
    }
}

using UnityEngine;

public enum ObjectType
{
    Item,
    Obstacle,
}

public enum ItemType
{
    
}

public enum ObstacleType
{

}

public class SpawnController : MonoBehaviour
{
    public ObjectPool pool;

    public string[] mapArray = { "CoinMap", "BaseMap", "errerMap" };

    private void Awake()
    {
        pool = GetComponent<ObjectPool>();

        for (int i = 0; i < 4; i++)
        {
            GetMapObject(pool).transform.position = new Vector3(0, 0, i * 30);
        }
    }

    public GameObject GetMapObject(ObjectPool inPool)
    {
        int randomMap = Random.Range(0, mapArray.Length);
        GameObject outNewMap = inPool.SpawnFromPool(mapArray[randomMap]);

        return outNewMap;
    }
}

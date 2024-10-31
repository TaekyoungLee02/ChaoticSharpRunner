using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Map = 0,
    Item = 10,
    Obstacle = 20,
}

public enum MapType
{
    CoinMap,
    BaseMap,
    errerMap,
    Max,
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

    private void Awake()
    {
        pool = GetComponent<ObjectPool>();

        for (int i = 0; i < 4; i++)
        {
            int randomMap = Random.Range((int)ObjectType.Map, (int)ObjectType.Map + (int)MapType.Max);
            GameObject newMap = pool.SpawnFromPool(randomMap);
            newMap.transform.position = new Vector3(0, 0, i * 30);
        }
    }
}

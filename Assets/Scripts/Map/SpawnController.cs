using System.Collections.Generic;
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
    public string[] mapNameArray = { "CoinMap", "BaseMap", "errerMap" };
    public List<GameObject> mapObjectArray = new List<GameObject>();

    [SerializeField]
    public int mapSpawnCount;
    [SerializeField]
    public float mapSpawnDistance;

    private void Awake()
    {
        for (int i = 0; i < mapSpawnCount; i++)
        { // 처음 초기화 시 리스트에 넣으면서 생성
            StartMapSpawn(i);
        }
    }

    private void StartMapSpawn(int inSpawnCount)
    {
        GameObject newMapObject = GetMapObject();
        newMapObject.transform.position =
            new Vector3(0, 0, inSpawnCount * mapSpawnDistance);
        mapObjectArray.Add(newMapObject);
    }

    public GameObject GetMapObject()
    { // 오브젝트 랜덤 생성
        int randomMap = Random.Range(0, mapNameArray.Length);
        GameObject outNewMap = ObjectPool.Instance.SpawnFromPool(mapNameArray[randomMap]);

        return outNewMap;
    }
}

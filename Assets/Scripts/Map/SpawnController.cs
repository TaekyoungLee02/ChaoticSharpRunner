using System.Collections.Generic;
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
    private string[] mapNameArray = { "CoinMap1", "CoinMap2", "BaseMap1",
    "BaseMap2", "errerMap1", "errerMap2"};
    public List<GameObject> mapObjectArray = new List<GameObject>();

    private float mapSpawnDistance = 30;
    private int mapSpawnCount = 6;

    private void Start()
    {
        GameManager.Instance.OnGameReset += InitializeMapPosition;
        GameManager.Instance.OnGameRestart += InitializeMapPosition;
        GameManager.Instance.OnGameStart += InitializeMapPosition;

        for (int i = 0; i < mapSpawnCount; i++)
        { // ó�� �ʱ�ȭ �� ����Ʈ�� �����鼭 ����
            StartMapSpawn(i);
        }
    }

    private void StartMapSpawn(int inSpawnCount)
    {
        GameObject newMapObject = GetMapObject();
        newMapObject.transform.position =
            new Vector3(0, -1, inSpawnCount * mapSpawnDistance);
        mapObjectArray.Add(newMapObject);
    }

    public GameObject GetMapObject()
    { // ������Ʈ ���� ����
        int randomMap = Random.Range(0, mapNameArray.Length);
        GameObject outNewMap = ObjectPool.Instance.SpawnFromPool(mapNameArray[randomMap]);
        
        ItemManager.Instance.SpawnItemInMap(outNewMap.GetComponent<MapScroller>());
        ObstacleManager.Instance.SpawnObstacleInMap(outNewMap.GetComponent<MapScroller>());

        return outNewMap;
    }

    public void InitializeMapPosition()
    {
        for (int i = 0; i < mapObjectArray.Count; i++)
        {
            mapObjectArray[i].transform.position =
                new Vector3(0, -1, i * mapSpawnDistance);
        }
    }
}

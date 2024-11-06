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
        Debug.Log(outNewMap);

        return outNewMap;
    }

    public void InitializeMapPosition()
    {
        for (int i = 0; i < mapObjectArray.Count; i++)
        {
            mapObjectArray[i].transform.position =
                new Vector3(0, 0, i * mapSpawnDistance);
        }
    }
}

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
    //public MapSpawnSetting mapSetting;

    [SerializeField]
    private string[] mapNameArray;
    public List<GameObject> mapObjectArray = new List<GameObject>();

    [SerializeField]
    private float mapSpawnDistance = 30;
    private int mapSpawnCount = 6;

    private void Start()
    {
        for (int i = 0; i < mapSpawnCount; i++)
        { // ó�� �ʱ�ȭ �� ����Ʈ�� �����鼭 ����
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
    { // ������Ʈ ���� ����
        int randomMap = Random.Range(0, mapNameArray.Length);
        GameObject outNewMap = ObjectPool.Instance.SpawnFromPool(mapNameArray[randomMap]);

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

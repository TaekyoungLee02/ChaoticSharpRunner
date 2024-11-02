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

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        { // 처음 초기화 시 리스트에 넣으면서 생성
            GameObject newMapObject = GetMapObject(ObjectPool.Instance);
            newMapObject.transform.position = new Vector3(0, 0, i * 30);
            mapObjectArray.Add(newMapObject);
        }
    }

    public GameObject GetMapObject(ObjectPool inPool)
    { // 오브젝트 랜덤 생성
        int randomMap = Random.Range(0, mapNameArray.Length);
        GameObject outNewMap = inPool.SpawnFromPool(mapNameArray[randomMap]);

        return outNewMap;
    }
}

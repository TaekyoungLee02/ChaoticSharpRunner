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
        { // ó�� �ʱ�ȭ �� ����Ʈ�� �����鼭 ����
            GameObject newMapObject = GetMapObject(ObjectPool.Instance);
            newMapObject.transform.position = new Vector3(0, 0, i * 30);
            mapObjectArray.Add(newMapObject);
        }
    }

    public GameObject GetMapObject(ObjectPool inPool)
    { // ������Ʈ ���� ����
        int randomMap = Random.Range(0, mapNameArray.Length);
        GameObject outNewMap = inPool.SpawnFromPool(mapNameArray[randomMap]);

        return outNewMap;
    }
}

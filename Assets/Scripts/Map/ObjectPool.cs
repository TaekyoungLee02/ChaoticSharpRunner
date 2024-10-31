using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public int key;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<int, Queue<GameObject>> poolDistionary;

    private void Awake()
    {
        poolDistionary = new Dictionary<int, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject newObject = Instantiate(pool.prefab, transform);
                newObject.SetActive(false);
                objectPool.Enqueue(newObject);
            }
            poolDistionary.Add(pool.key, objectPool);
        }
    }


    public GameObject SpawnFromPool(int inKey)
    {
        if (!poolDistionary.ContainsKey(inKey))
        {
            return null;
        }
        Queue<GameObject> objectPool = poolDistionary[inKey];

        int count = objectPool.Count;
        for (int i = 0; i < objectPool.Count; i++)
        {
            GameObject outObj = objectPool.Dequeue();

            if (!outObj.activeInHierarchy)
            {
                outObj.SetActive(true);
                objectPool.Enqueue(outObj);
                return outObj;
            }

            count++;
            objectPool.Enqueue(outObj);
        }

        if (count == objectPool.Count)
        {
            GameObject outNewObj = Instantiate(objectPool.Peek());
            outNewObj.SetActive(true);
            objectPool.Enqueue(outNewObj);

            return outNewObj;
        }

        return null;
    }
}

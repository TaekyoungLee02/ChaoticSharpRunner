using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDistionary;

    private void Awake()
    {
        poolDistionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject newObject = Instantiate(pool.prefab, transform);
                newObject.SetActive(false);
                objectPool.Enqueue(newObject);
            }
            poolDistionary.Add(pool.tag, objectPool);
        }
    }


    public GameObject SpawnFromPool(string inTag)
    {
        if (!poolDistionary.ContainsKey(inTag))
        {
            return null;
        }
        Queue<GameObject> objectPool = poolDistionary[inTag];

        int count = objectPool.Count;
        for (int i = 0; i < objectPool.Count; i++)
        {
            GameObject outObject = objectPool.Dequeue();

            if (!outObject.activeInHierarchy)
            {
                outObject.SetActive(true);
                objectPool.Enqueue(outObject);
                return outObject;
            }

            count++;
            objectPool.Enqueue(outObject);
        }

        if (count == objectPool.Count)
        {
            GameObject outNewObject = Instantiate(objectPool.Peek());
            outNewObject.SetActive(true);
            objectPool.Enqueue(outNewObject);

            return outNewObject;
        }

        return null;
    }
}

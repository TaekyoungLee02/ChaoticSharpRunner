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
        public Transform spawnPosition;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public override void Awake()
    {
        base.Awake();

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject newObject = Instantiate(pool.prefab, pool.spawnPosition);
                newObject.SetActive(false);
                objectPool.Enqueue(newObject);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }


    public GameObject SpawnFromPool(string inTag)
    {
        if (!poolDictionary.ContainsKey(inTag))
        {
            return null;
        }
        Queue<GameObject> objectPool = poolDictionary[inTag];

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

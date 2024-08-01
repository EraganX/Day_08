using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolManager : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string nameObj;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                obj.transform.SetParent(transform);
            }

            poolDictionary.Add(pool.nameObj, objectPool);
        }
    }

    public GameObject GetPooledObject(string nameObj)
    {
        if (!poolDictionary.ContainsKey(nameObj))
        {
            Debug.LogWarning("Pool with tag " + nameObj + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[nameObj].Dequeue();  //remove from start
        objectToSpawn.SetActive(true);
        poolDictionary[nameObj].Enqueue(objectToSpawn); // add again to the end

        return objectToSpawn;
    }

    public void ReturnPoolObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}

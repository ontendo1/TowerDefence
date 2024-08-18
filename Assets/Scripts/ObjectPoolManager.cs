using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPoolManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] private List<Pool> pools = new();
    public Dictionary<string, Queue<GameObject>> poolDictionary = new();


    void Start()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.name, objectPool);
        }
    }

    //Use this instead of Instantiate()
    public GameObject SpawnFromPool(string name, Vector3 position, Quaternion rotation, GameObject prefab)
    {
        if (poolDictionary[name].Count == 0)
        {
            GameObject obj = Instantiate(prefab);
            poolDictionary[name].Enqueue(obj);
        }

        GameObject objectToSpawn = poolDictionary[name].Dequeue();

        objectToSpawn.transform.SetPositionAndRotation(position, rotation);
        objectToSpawn.SetActive(true);

        if (objectToSpawn.TryGetComponent(out IPoolableObject poolableObject))
        {
            poolableObject.OnSpawn();
        }

        return objectToSpawn;
    }

    //Use this instead of Destroy();
    public void AddToPool(string name, GameObject obj)
    {
        poolDictionary[name].Enqueue(obj);
        obj.SetActive(false);
    }
}


public interface IPoolableObject
{
    public void OnSpawn();
}
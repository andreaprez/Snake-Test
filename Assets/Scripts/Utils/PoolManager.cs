using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
    public static PoolManager instance;
    [SerializeField] private List<ObjectPool> listOfPools;
    
    void Awake() {
        instance = this;
    }

    void Start() {
        if (listOfPools != null) {
            for (int i = 0; i < listOfPools.Count; i++) {
                FillPool(listOfPools[i]);
            }
        }
    }

    void FillPool(ObjectPool pool) {
        for (int i = 0; i < pool.amount; i++) {
            pool.Initialize();
            var tmpInstance = Instantiate(pool.prefab, pool.container.transform);
            tmpInstance.SetActive(false);
            tmpInstance.transform.position = pool.container.transform.position;
            pool.objects.Add(tmpInstance);
        }
    }
    
    public GameObject GetPoolObject(ObjectPoolType type) {
        ObjectPool pool = GetPoolByType(type);
        List<GameObject> poolObjects = pool.objects;

        if (poolObjects != null && poolObjects.Count > 0)
        {
            GameObject selectedObj = null;
            for (int i = 0; i < poolObjects.Count; i++) {
                if (!poolObjects[i].activeInHierarchy)
                {
                    selectedObj = poolObjects[i];
                }
            }
            
            // Move the selected object to the start of the pool to keep the pool organised.
            if (selectedObj != null) {
                poolObjects.Remove(selectedObj);
                poolObjects.Insert(0, selectedObj);
                return selectedObj;
            }

            return poolObjects[poolObjects.Count - 1]; // If every object in the pool is active, it will return the one that has been active for longer.
        }
        return null;
    }
    
    public ObjectPool GetPoolByType(ObjectPoolType type) {
        for (int i = 0; i < listOfPools.Count; i++) {
            if (type.Equals(listOfPools[i].type)) {
                return listOfPools[i];
            }
        }
        return null;
    }

    public void ResetPoolObject(GameObject obj, ObjectPoolType type) {
        obj.SetActive(false);
        
        ObjectPool pool = GetPoolByType(type);
        if (pool != null) {
            obj.transform.position = pool.container.transform.position;
            
            // Move the cleared object to the end of the pool to keep the pool organised.
            pool.objects.Remove(obj);
            pool.objects.Add(obj);
        }
    }
}

[Serializable]
public class ObjectPool {
    public ObjectPoolType type;
    public GameObject prefab;
    public GameObject container;
    public int amount;

    public List<GameObject> objects = new List<GameObject>();

    public void Initialize() {
        switch (type) {
            case ObjectPoolType.Food:
                prefab = GameConfig.GetAssetsConfiguration().FoodApplePrefab;
                break;
        }
    }
}

public enum ObjectPoolType
{
    Audio,
    Food
}
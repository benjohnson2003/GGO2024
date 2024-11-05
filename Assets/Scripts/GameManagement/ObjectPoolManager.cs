using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public static List<PooledObjectInfo> objectPools = new List<PooledObjectInfo>();

    private GameObject _poolEmptyHolder;
    private static List<GameObject> _poolEmpties = new List<GameObject>();

    public enum PoolType
    {
        particle,
        gameObject,
        none
    }
    public static PoolType poolType;

    protected override void Awake()
    {
        base.Awake();
        SetupEmpties();
    }


    private void SetupEmpties()
    {
        _poolEmptyHolder = new GameObject("Pooled Objects");
        _poolEmptyHolder.transform.parent = transform;

        // Create empty holders
        for (int i = 0; i < Enum.GetValues(typeof(PoolType)).Length - 1; i++)
        {
            _poolEmpties.Add(new GameObject(((PoolType)i).ToString()));
            _poolEmpties[i].transform.SetParent(_poolEmptyHolder.transform);
        }
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType _poolType = PoolType.none)
    {
        PooledObjectInfo pool = objectPools.Find(p => p.lookupString == objectToSpawn.name);

        // If pool doesnt exist, create pool
        if (pool == null)
        {
            pool = new PooledObjectInfo() { lookupString = objectToSpawn.name };
            objectPools.Add(pool);
        }

        // Check for inactive objects in pool
        GameObject spawnableObj = null;
        foreach (GameObject obj in pool.inactiveObjects)
        {
            if (obj != null)
            {
                spawnableObj = obj;
                break;
            }
        }

        if (spawnableObj == null)
        {
            GameObject parentObject = SetParentObject(_poolType);

            // No inactive objects
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);

            if (parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            // Inactive object found
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.inactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Substring(0, obj.name.Length - 7); // Remove clone from name

        PooledObjectInfo pool = objectPools.Find(p => p.lookupString == goName);

        if (pool == null)
        {
            Debug.LogWarning("Trying to release an object that is not pooled " + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.inactiveObjects.Add(obj);
        }
    }

    private static GameObject SetParentObject(PoolType _poolType)
    {
        if (_poolType == PoolType.none)
            return null;

        return _poolEmpties[(int)_poolType];
    }
}

public class PooledObjectInfo
{
    public string lookupString;
    public List<GameObject> inactiveObjects = new List<GameObject>();
}
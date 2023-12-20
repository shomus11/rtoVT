using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    private List<GameObject> PooledObjs = new List<GameObject>();
    [SerializeField] private int totalPooled = 10;
    [SerializeField] private GameObject pooledObj;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start() {
        InitPool();
    }

    private void InitPool()
    {
        for (int i = 0; i < totalPooled; i++)
        {
            GameObject obj = Instantiate(pooledObj);
            obj.transform.SetParent(gameObject.transform);
            obj.SetActive(false);
            PooledObjs.Add(obj);
        }
    }

    public GameObject GetPooledObj()
    {
        for (int i = 0; i < PooledObjs.Count; i++)
        {
            if (!PooledObjs[i].activeInHierarchy)
                return PooledObjs[i];
        }
        return null;
    }
}

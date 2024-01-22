using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler sharedInstance;

    [Header("sfx Component")]
    public List<GameObject> sfxPooledObject;
    public GameObject sfxPrefabs;
    public int sfxAmountToPool;
    [Space(10)]

    [Header("projectile Component")]
    public List<GameObject> projectilePooledList;
    public GameObject projectilePrefabs;
    public int projectileAmountToPool;

    void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        sfxPooledObject = new List<GameObject>();
        projectilePooledList = new List<GameObject>();
        InitSpawnObject(sfxPrefabs, sfxPooledObject, sfxAmountToPool);
        InitSpawnObject(projectilePrefabs, projectilePooledList, projectileAmountToPool);

    }

    private void Update()
    {

    }

    public void InitSpawnObject(GameObject prefabs, List<GameObject> list, int amount)
    {
        GameObject tmp;
        for (int i = 0; i < sfxAmountToPool; i++)
        {
            tmp = Instantiate(prefabs, gameObject.transform);
            tmp.SetActive(false);
            list.Add(tmp);
        }
    }

    public GameObject GetPooledObject(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeInHierarchy)
            {
                return list[i];
            }
        }
        return null;
    }



}
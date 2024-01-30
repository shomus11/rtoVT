using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler sharedInstance;

    [Header("sfx Component")]
    public List<GameObject> sfxPooledObject;
    public GameObject sfxPrefabs;
    public int sfxAmountToPool;
    public Transform sfxContainer;
    [Space(10)]

    [Header("Player Projectile Component")]
    public List<GameObject> projectilePooledList;
    public GameObject projectilePrefabs;
    public int projectileAmountToPool;
    public Transform projectTileContainer;

    [Header("Enemy projectile Component")]
    public List<GameObject> EnemyProjectilePooledList;
    public GameObject EnemyProjectilePrefabs;
    public int EnemyProjectileAmountToPool;
    public Transform EnemyProjectileContainer;

    void Awake()
    {
        sharedInstance = this;
        sfxPooledObject = new List<GameObject>();
        projectilePooledList = new List<GameObject>();
        EnemyProjectilePooledList = new List<GameObject>();
        InitSpawnObject(sfxPrefabs, sfxPooledObject, sfxAmountToPool, sfxContainer);
        InitSpawnObject(EnemyProjectilePrefabs, EnemyProjectilePooledList, EnemyProjectileAmountToPool, EnemyProjectileContainer);
        InitSpawnObject(projectilePrefabs, projectilePooledList, projectileAmountToPool, projectTileContainer);
    }

    void Start()
    {

    }

    private void Update()
    {

    }

    public void InitSpawnObject(GameObject prefabs, List<GameObject> list, int amount, Transform container)
    {
        GameObject tmp;
        for (int i = 0; i < sfxAmountToPool; i++)
        {
            tmp = Instantiate(prefabs, container);
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
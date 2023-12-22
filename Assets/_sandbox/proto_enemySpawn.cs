using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class proto_enemySpawn : MonoBehaviour
{
    public GameObject enemyPrefabs;
    public int maxEnemy = 6;
    public float totalEnemy;
    public List<WavePattern> row = new List<WavePattern>();
    public Transform[ , ] spawnArea;
    public Transform baseSpawn;
    public int rowEnemy = 3;
    public int columEnemy = 10;

    public proto_enemy[] enemies;

    public TextMeshProUGUI debugTxt;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemies = FindObjectsOfType<proto_enemy>();

        for (int i = 0; i < row.Count; i++)
        {
            for (int j = 0; j < row[i].columb.Length; j++)
            {
                if (row[i].columb[j] == 1)
                {
                    Instantiate(enemyPrefabs, row[i].pos[j]);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public struct WavePattern
{
    public int[] columb;
    public Transform[] pos;
}

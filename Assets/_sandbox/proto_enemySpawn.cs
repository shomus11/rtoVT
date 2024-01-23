using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class proto_enemySpawn : MonoBehaviour
{
    public GameObject enemyPrefabs;
    public int maxEnemy = 30;
    public float totalEnemy;
    public List<WavePattern> row = new List<WavePattern>();
    public Transform[,] spawnArea;
    public Transform baseSpawn;
    int rowEnemy = 3;
    int columEnemy = 10;

    [Header("Enemies List")]
    public List<proto_enemy> enemies;
    [Space(10)]

    [Header("Enemies Position setup")]
    [SerializeField] List<Vector3> enemiesfixPosition;
    [SerializeField] List<Vector3> enemiesStartPosition;
    [Space(10)]

    public TextMeshProUGUI debugTxt;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        //setPosition list first -> setup npc coming from -> spawn npc and add to npc list ->  npc
        InitNpcFixPosition();
        InitNPCStartPosition();
        InitNPC();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitNPC()
    {
        SpawnNPC();
    }
    void InitNpcFixPosition()
    {
        enemiesfixPosition = new List<Vector3>();
        for (int i = 0; i < row.Count; i++)
        {
            for (int j = 0; j < row[i].columb.Length; j++)
            {
                if (row[i].columb[j] == 1)
                {
                    enemiesfixPosition.Add(row[i].pos[j].localPosition);
                }
            }
        }
    }

    void InitNPCStartPosition()
    {
        enemiesStartPosition = new List<Vector3>();
        for (int i = 0; i < enemiesfixPosition.Count; i++)
        {
            if (i < (enemiesfixPosition.Count / 2))
            {
                enemiesStartPosition.Add(
                  new Vector3(
                      enemiesfixPosition[i].x - 10f,
                      enemiesfixPosition[i].y,
                      enemiesfixPosition[i].z)
                  );
            }
            else
            {
                enemiesStartPosition.Add(
                 new Vector3(
                     enemiesfixPosition[i].x + 10f,
                     enemiesfixPosition[i].y,
                     enemiesfixPosition[i].z)
                 );
            }
        }
    }
    void SpawnNPC()
    {
        enemies = new List<proto_enemy>();
        for (int i = 0; i < enemiesStartPosition.Count; i++)
        {
            proto_enemy enemy = Instantiate(enemyPrefabs, transform).GetComponent<proto_enemy>();
            enemies.Add(enemy);
            enemies[i].transform.localPosition = enemiesStartPosition[i];
            // do move to fix location
        }
    }
    public void NPCPattern()
    {

    }

}


[Serializable]
public struct WavePattern
{
    public int[] columb;
    public Transform[] pos;
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class proto_enemySpawn : MonoBehaviour
{
    public static proto_enemySpawn Instance;

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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        //setPosition list first -> setup npc coming from -> spawn npc and add to npc list ->  npc
        InitNpcFixPosition();
        InitNPCStartPosition();
        InitNPC();
        InvokeRepeating("MoveNPC", 5f, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (AreAllEnemiesDisabled())
        {
            enemies.Clear();
            StopAllCoroutines();
            StartCoroutine(WaitTimeForSecond(4f));
            InitNPC();
        }
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
        NPCPattern();
    }

    bool AreAllEnemiesDisabled()
    {
        return enemies.All(enemy => !enemy.gameObject.activeSelf);
    }

    IEnumerator WaitTimeForSecond(float value)
    {
        yield return new WaitForSeconds(value);
    }

    public void NPCPattern()
    {
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                ;
                if (i < enemies.Count / 2)
                {
                    StartCoroutine(SetNPCAnimation(i, "Enemy_3_Turn_Right"));
                }
                else
                {
                    StartCoroutine(SetNPCAnimation(i, "Enemy_3_Turn_Left"));
                }
            }
        }


    }

    IEnumerator SetNPCAnimation(int value, string animationName)
    {
        Debug.Log("jalan animasi");
        enemies[value].PlayNPCAnimation(animationName);
        enemies[value].transform.DOLocalMove(enemiesfixPosition[value], 2f).From(enemiesStartPosition[value]);
        yield return new WaitForSeconds(2f);
        enemies[value].PlayNPCAnimation("Enemy_3_Idle");

    }

    IEnumerator WaitBeforeBackLeft(int i)
    {
        yield return new WaitForSeconds(2f);
        enemies[i].transform.DOLocalMove(enemiesfixPosition[i], 2f).From(enemiesfixPosition[i] + new Vector3(5f, 0, 0));
    }
    IEnumerator WaitBeforeBackRight(int i)
    {
        yield return new WaitForSeconds(2f);
        enemies[i].transform.DOLocalMove(enemiesfixPosition[i], 2f).From(enemiesfixPosition[i] + new Vector3(-5f, 0, 0));
    }
    void MoveNPC()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                continue;
            }
            if (i % 3 == 0 || i % 3 == 2)
            {
                enemies[i].transform.DOLocalMove(enemiesfixPosition[i] + new Vector3(-5f, 0, 0), 2f).From(enemiesfixPosition[i]);
                Debug.Log("Enemy " + i + " move left");

                StartCoroutine(WaitBeforeBackRight(i));


            }
            if (i % 3 == 1)
            {
                enemies[i].transform.DOLocalMove(enemiesfixPosition[i] + new Vector3(5f, 0, 0), 2f).From(enemiesfixPosition[i]);
                Debug.Log("Enemy " + i + " move right");

                StartCoroutine(WaitBeforeBackLeft(i));

            }
            // else if (i % 2 == 0 && i >= enemiesfixPosition.Count / 2)
            // {
            //     enemies[i].transform.DOLocalMove(enemiesfixPosition[i] + new Vector3(5f, 0, 0), 2f).From(enemiesfixPosition[i]);
            // }
            // else if (i % 2 == 1 && i >= enemiesfixPosition.Count / 2)
            // {
            //     enemies[i].transform.DOLocalMove(enemiesfixPosition[i] + new Vector3(-5f, 0, 0), 2f).From(enemiesfixPosition[i]);
            // }

        }
    }


}


[Serializable]
public struct WavePattern
{
    public int[] columb;
    public Transform[] pos;
}

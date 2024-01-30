using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public GameObject enemyPrefabs;
    public int maxEnemy = 30;
    public float totalEnemy;
    public List<WavePattern> row = new List<WavePattern>();
    public Transform[,] spawnArea;
    public Transform baseSpawn;
    int rowEnemy = 3;
    int columEnemy = 10;

    [Header("Enemies List")]
    public List<EnemyBehavior> enemies;
    [Space(10)]

    [Header("Enemies Position setup")]
    [SerializeField] List<Vector3> enemiesfixPosition;
    [SerializeField] List<Vector3> enemiesStartPosition;
    [Space(10)]

    [Header("Enemies movement Component")]
    [SerializeField] float movementTimerMin = 3f;
    [SerializeField] float movementTimerMax = 5f;

    [SerializeField] float movementTimer = 5f;
    [SerializeField] float movementCooldown;
    public bool isCooldown = false;
    public bool canMove = false;
    public bool ableToMove = false;

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
        movementTimer = UnityEngine.Random.Range(movementTimerMin, movementTimerMax);
        movementCooldown = UnityEngine.Random.Range(movementTimerMin, movementTimerMax);
        //movementTimerTemp = UnityEngine.Random.Range(movementTimerMin, movementTimerMax);
    }

    // Update is called once per frame
    void Update()
    {
        //if (AreAllEnemiesDisabled())
        //{
        //    enemies.Clear();
        //    StopAllCoroutines();
        //    StartCoroutine(WaitTimeForSecond(4f));
        //    InitNPC();
        //}
        MoveNPC();
    }

    public void InitNPC()
    {
        InitNpcFixPosition();
        InitNPCStartPosition();
        SpawnNPC();
        //InvokeRepeating("MoveNPC", 5f, 6f);
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
        enemies = new List<EnemyBehavior>();
        for (int i = 0; i < enemiesStartPosition.Count; i++)
        {
            EnemyBehavior enemy = Instantiate(enemyPrefabs, transform).GetComponent<EnemyBehavior>();
            enemies.Add(enemy);
            enemies[i].transform.localPosition = enemiesStartPosition[i];
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
        enemies[value].PlayNPCAnimation(animationName);
        enemies[value].transform.DOLocalMove(enemiesfixPosition[value], 2f).From(enemiesStartPosition[value]);
        yield return new WaitForSeconds(2f);
        enemies[value].PlayNPCAnimation("Enemy_3_Idle");
        ableToMove = true;
    }

    void MoveNPC()
    {
        if (GameManager.instance.gameState == GameStates.Pause)
        {
            return;
        }
        if (GameManager.instance.gameState == GameStates.Gameplay)
        {
            if (ableToMove)
            {
                if (!isCooldown)
                {
                    movementTimer -= Time.deltaTime;
                    if (movementTimer <= 0)
                    {
                        canMove = true;
                        isCooldown = true;
                    }
                }

                if (isCooldown)
                {
                    movementCooldown -= Time.deltaTime;
                    if (movementCooldown <= 0)
                    {
                        isCooldown = false;
                        movementTimer = UnityEngine.Random.Range(movementTimerMin, movementTimerMax); ;
                        movementCooldown = UnityEngine.Random.Range(movementTimerMin, movementTimerMax); ;
                    }
                }

                if (canMove)
                {
                    SetNPCToMove();
                }
            }
        }
    }

    void SetNPCToMove()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                continue;
            }
            if (i % 3 == 0 || i % 3 == 2)
            {
                enemies[i].MoveNPC(enemiesfixPosition[i] + new Vector3(-5f, 0, 0), enemiesfixPosition[i], "Enemy_3_Turn_Left", "Enemy_3_Turn_Right", "Enemy_3_Idle");
                //StartCoroutine(WaitBeforeBackRight(i));
            }
            if (i % 3 == 1)
            {
                enemies[i].MoveNPC(enemiesfixPosition[i] + new Vector3(5f, 0, 0), enemiesfixPosition[i], "Enemy_3_Turn_Right", "Enemy_3_Turn_Left", "Enemy_3_Idle");
                //StartCoroutine(WaitBeforeBackLeft(i));
            }
        }
        canMove = false;
    }


}


[Serializable]
public struct WavePattern
{
    public int[] columb;
    public Transform[] pos;
}

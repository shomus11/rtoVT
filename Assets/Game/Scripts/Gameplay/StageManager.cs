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

    [Header("Stage Data")]
    public StageData stageDatas;
    [SerializeField] int currentStage = -1;
    bool switchStage = false;
    [SerializeField] bool checkNPC = false;

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
    [SerializeField] List<EnemyBehavior> enemiesTemp;
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

    public int CurrentStage { get => currentStage; set => currentStage = value; }

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
        InitNpcFixPosition();
        InitNPCStartPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkNPC)
            MoveNPC();
    }

    public void DefeatEnemy(EnemyBehavior target)
    {
        target.gameObject.SetActive(false);
        if (AreAllEnemiesDisabled())
        {
            ResetNPC();
            InitNPC();
        }
    }

    public void CheckTotalNPC()
    {
        if (enemies.Count == 0)
        {
            InitNPC();
        }
    }

    public void ResetNPC()
    {
        Debug.Log("Reseting");
        enemiesTemp = enemies;

        enemies = new List<EnemyBehavior>();

        for (int i = 0; i < enemiesTemp.Count; i++)
        {
            if (enemiesTemp[i].gameObject.activeInHierarchy)
            {
                Destroy(enemiesTemp[i].gameObject);
            }

        }
    }

    public void InitNPC()
    {
        checkNPC = false;
        InitNPCData();
        ResetNPC();
        StartCoroutine(InitiatingSpawnNPC(stageDatas.stageSetups[CurrentStage].SpawnDelay));
    }

    IEnumerator InitiatingSpawnNPC(float delay)
    {

        yield return new WaitForSeconds(delay);
        SpawnNPC();
        NPCPattern();
        movementTimer = UnityEngine.Random.Range(movementTimerMin, movementTimerMax);
        movementCooldown = UnityEngine.Random.Range(movementTimerMin, movementTimerMax);
        checkNPC = true;
    }

    public void InitNPCData()
    {
        if (currentStage < stageDatas.stageSetups.Count - 1)
        {
            CurrentStage++;
            enemyPrefabs = stageDatas.stageSetups[CurrentStage].enemyPrefabs;
        }
        else
        {
            //win
            GameManager.instance.FadeOut("Victory");
        }
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
    }

    bool AreAllEnemiesDisabled()
    {
        if (checkNPC)
            return enemies.All(enemy => !enemy.gameObject.activeSelf);
        else
            return false;

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
                    StartCoroutine(SetNPCAnimation(i, "EnemyTurnRight"));
                }
                else
                {
                    StartCoroutine(SetNPCAnimation(i, "EnemyTurnLeft"));
                }
            }
        }


    }

    IEnumerator SetNPCAnimation(int value, string animationName)
    {
        if (enemies[value].isActiveAndEnabled)
            enemies[value].PlayNPCAnimation(animationName);

        if (enemies[value].isActiveAndEnabled)
            enemies[value].transform.DOLocalMove(enemiesfixPosition[value], 2f).From(enemiesStartPosition[value]);

        yield return new WaitForSeconds(2f);

        if (enemies[value].isActiveAndEnabled)
            enemies[value].PlayNPCAnimation("EnemyIdle");

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
                enemies[i].MoveNPC(enemiesfixPosition[i] + new Vector3(-5f, 0, 0), enemiesfixPosition[i], "EnemyTurnLeft", "EnemyTurnRight", "EnemyIdle");
            }
            if (i % 3 == 1)
            {
                enemies[i].MoveNPC(enemiesfixPosition[i] + new Vector3(5f, 0, 0), enemiesfixPosition[i], "EnemyTurnRight", "EnemyTurnLeft", "EnemyIdle");
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

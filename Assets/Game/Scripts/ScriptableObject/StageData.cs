using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EnemyMovementPattern
{
    pattern1,
    pattern2,
    pattern3,
}

[System.Serializable]
public struct StageSetup
{
    public float SpawnDelay;
    public GameObject enemyPrefabs;
    public EnemyMovementPattern pattern;
}

[System.Serializable]
[CreateAssetMenu(fileName = "StageData", menuName = "SO/StageData")]
public class StageData : ScriptableObject
{
    public List<StageSetup> stageSetups;
}

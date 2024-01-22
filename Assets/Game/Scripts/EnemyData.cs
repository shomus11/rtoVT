using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Small,
    Boss
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/Player")]
public class EnemyData : ScriptableObject
{
   public EnemyType enemyType;
   public float enemyHealthPoint;

}

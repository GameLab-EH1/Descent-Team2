using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObj", menuName = "ScriptableObjs/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Statistic Variables")]
    public int Hp;
    public float FireDelay;
    public float MovementSpeed;
    public int Dmg;
    
    [Header("Game Mecchanics Variables")]
    public int score;
    public GameObject[] PUp_droppable;
    public int PUp_DropRate;
    
    [Header("Moving Variables")]
    public float TimeBeforeMoving;
    public float VisualRange;
    public float VisualAngle;
    public float StoppingDistance;
    public float RotAroundDelay;
    public bool IsChasingDrone;
    
    [Header("Shooting Variables")]
    public GameObject BulletPref;
    public LayerMask PlayerLayer;

}

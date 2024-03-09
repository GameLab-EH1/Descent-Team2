using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObj", menuName = "ScriptableObjs/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Statistic Variables")]
    public int Hp;
    public float FireRate;
    public float MovementSpeed;
    
    [Header("Physics Variables")]
    public float KnockbackForce;
    
    [Header("Game Mecchanics Variables")]
    public int score;
    public int PUp_DropRate;
    
    [Header("Moving Variables")]
    public float TimeBeforeMoving;
    public float VisualRange;
    public float VisualAngle;
    public float StoppingDistance;
    public float RotAroundDelay;
    
    [Header("Shooting Variables")]
    public GameObject BulletPref;
    public LayerMask PlayerLayer;
    
}

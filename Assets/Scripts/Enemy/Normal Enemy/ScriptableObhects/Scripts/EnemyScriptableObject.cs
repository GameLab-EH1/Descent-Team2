using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObj", menuName = "ScriptableObjs/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public int Hp;
    public int score;
    public int PUp_DropRate;
    public float FireRate;
    public float MovementSpeed;
    public float KnockbackForce;
    public float TimeBeforeMoving;
    public float VisualRange;
    public float VisualAngle;
    public float StoppingDistance;
    public GameObject BulletPref;
}

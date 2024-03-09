using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthManager : MonoBehaviour
{
    [SerializeField] EnemyScriptableObject _scriptableObject;
    [HideInInspector]public int HP;

    private void Awake()
    {
        HP = _scriptableObject.Hp;
    }
    public void GotDmg(int dmg)
    {
        HP -= dmg;
    }
}

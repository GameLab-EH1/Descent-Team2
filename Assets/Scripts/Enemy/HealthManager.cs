using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthManager : MonoBehaviour
{
    [SerializeField] EnemyScriptableObject _scriptableObject;
    public int HP;
    [SerializeField] private ClassDrone1 _classDrone1;

    private void Awake()
    {
        HP = _scriptableObject.Hp;
    }
    public void GotDmg(int dmg)
    {
        HP -= dmg;
        _classDrone1.SwitchState(_classDrone1.ChasingState);
    }
}

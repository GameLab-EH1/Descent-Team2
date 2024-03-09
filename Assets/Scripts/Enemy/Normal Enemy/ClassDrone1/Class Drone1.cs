using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClassDrone1 : MonoBehaviour
{
    //states
    private CurrentState _currentState;
    public PatrollingState PatrollingState;
    public ChasingState ChasingState;
    
    //Movement
    public Transform[] PatrollingPoints;
    
    //player ref
    public ShipController _ShipController;
    
    //stats
    [SerializeField] private HealthManager _healthManager;

    [SerializeField] private WeaponsManager _weaponsManager;
    
    
    public EnemyScriptableObject _scriptableObject;

    private void Awake()
    {
        ChasingState = new ChasingState(this);
        PatrollingState = new PatrollingState(this);
    }
    private void Start()
    {
        
        _currentState = PatrollingState;
        _currentState.EnterState(this);
    }
    private void Update()
    {
        _currentState.UpdateState(this);
        if (_healthManager.HP <= 0)
        {
            Debug.Log("i'm dead");
            
        }
    }
    public void SwitchState(CurrentState state)
    {
        _currentState = state;
        state.EnterState(this);
    }
    
    
}

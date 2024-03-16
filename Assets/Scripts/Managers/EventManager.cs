using System;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    //ui
    public static Action<int> OnWeaponSwap;
    public static Action<int> OnShooting;

    public static Action<bool> OnLaserShooting;
    public static Action OnLaserNoBullet;
    
    public static Action<int> OnPowerChange;
    public static Action<int> OnPowerPickup;

    public static Action<int> OnHealthChange;
    public static Action<int> OnShieldChange;

    public static Action OnRedKeyPickup;

    public static Action<int> onScoreChange;

    private void OnEnable()
    {
        onScoreChange += DebugFun;
    }

    private void DebugFun(int scoreAdded)
    {
        Debug.Log(scoreAdded);
    }
}

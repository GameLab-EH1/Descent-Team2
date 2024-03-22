using System;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    public static Action<int> OnWeaponSwap;
    public static Action<int> OnShooting;
    public static Action<int> OnFireRocket;

    public static Action<bool> OnLaserShooting;
    public static Action OnLaserNoBullet;
    
    public static Action<int> OnPowerChange;
    public static Action<int, bool> OnPowerPickup;
    
    public static Action OnPowerBoostLvl;
    
    public static Action<int> OnShieldChange;
    public static Action<int> OnShieldPickOrDmg;

    public static Action OnRedKeyPickup;

    public static Action<int> onScoreChange;

    public static Action<bool> OnGameEnd;

    public static Action OnBossDeath;

}

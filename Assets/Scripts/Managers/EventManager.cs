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

    public static Action<int> OnHealthChange;
    public static Action<int> OnShieldChange;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _creditsMenu, settingsMenu, mainMenu;


    public void Credits(bool isToShow)
    {
        if (isToShow)
        {
            mainMenu.SetActive(false);
        }
        else
        {
            mainMenu.SetActive(true);
        }
        
        _creditsMenu.SetActive(isToShow);
    }
    
    
    
    
    
}

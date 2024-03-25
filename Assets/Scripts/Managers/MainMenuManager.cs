using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _creditsMenu, _settingsMenu, _mainMenu, _controlMenu;


    public void Credits(bool isToShow)
    {
        if (isToShow)
        {
            _mainMenu.SetActive(false);
        }
        else
        {
            _mainMenu.SetActive(true);
        }
        
        _creditsMenu.SetActive(isToShow);
    }

    public void Settings(bool isToShow)
    {
        if (isToShow)
        {
            _mainMenu.SetActive(false);
        }
        else
        {
            _mainMenu.SetActive(true);
        }
        
        _settingsMenu.SetActive(isToShow);
    }
    public void Controlls(bool isToShow)
    {
        if (isToShow)
        {
            _mainMenu.SetActive(false);
        }
        else
        {
            _mainMenu.SetActive(true);
        }
        
        _controlMenu.SetActive(isToShow);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void playGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    
    
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaxPresentationShips : MonoBehaviour
{
    [SerializeField] private GameObject[] _images;
    private bool _isControllerButtonPressed;
    private int _index;
    [SerializeField] private GameObject story2;
    


    private void Update()
    {
        for (int i = 0;i < 20; i++) 
        {
            if(Input.GetKeyDown("joystick 1 button "+i))
            {
                _isControllerButtonPressed = true;
            }
        }
        if (_index < _images.Length)
        {
            FirstScrollingStory();
        }
        else
        {
            gameObject.SetActive(false);
            story2.SetActive(true);
        }
        
    }

    private void FirstScrollingStory()
    {
        if (Input.anyKeyDown || _isControllerButtonPressed)
        {
            _images[_index].SetActive(false);
            _index++;
            _isControllerButtonPressed = false;
        }
    }
}

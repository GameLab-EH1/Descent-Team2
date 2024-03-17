using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject leaderBoardTable;

    public void ShowLeaderBoard()
    {
        leaderBoardTable.SetActive(true);
        gameObject.SetActive(false);
    }
}

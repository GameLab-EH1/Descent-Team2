using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
public class HighScoreTable : MonoBehaviour
{

    [SerializeField] private List<TextMeshProUGUI> _names;
    [SerializeField] private List<TextMeshProUGUI> _scores;
    [SerializeField] private GameObject _menuButton;

    private string _leaderboardKey = "23463ccbe17a46b0b5ae4f2ebe136e032e18f055d1a2276d1c74b6013e1ad38b";
    private void Start()
    {
        GetLeaderBoard();
    }
    
    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(_leaderboardKey, ((msg) =>
        {
            int loopLenght = (msg.Length < _names.Count) ? msg.Length : _names.Count;
            for (int i = 0; i < loopLenght; i++)
            {
                _names[i].text = msg[i].Username;
                _scores[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboard(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(_leaderboardKey, username, score, ((msg) =>
        {
            username.Substring(0, 4);
            GetLeaderBoard();
        }));
    }
    public void mainMenuButton()
    {
        _menuButton.SetActive(true);
    }
}

using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public bool isGameEnd;
    void Start()
    {
        C_SpawnplayerReq req = new C_SpawnplayerReq();
        req.IsTopPlayer = NetworkManager.Instance.isTopPosition;
        NetworkManager.Instance.Send(req);
    }
    void Update()
    {
        
    }

    /// <summary>
    /// °ÔÀÓÀÌ Á¾·áµÈ »óÈ²
    /// </summary>
    /// <param name="win">승패 확인 bool 변수</param>
    public void GameOver(bool win)
    {
        OutComeUI.Instance.ShowOutcomePanel(win);
    }

    public void ExitGame()
    {
        C_LeaveRoom leave = new C_LeaveRoom();
        NetworkManager.Instance.Send(leave);

        SceneManager.LoadScene("LobbyScene");
    }
}

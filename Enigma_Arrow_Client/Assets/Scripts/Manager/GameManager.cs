using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public bool isGameEnd;
    void Start()
    {
        C_SpawnplayerReq req = new C_SpawnplayerReq();
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
}

using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : Singleton<LobbyManager>
{

    [SerializeField] GameObject _onMatchingPanel;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnMatchBtnClick()
    {
        _onMatchingPanel.SetActive(true);
        Match(false);
    }

    void Match(bool isCancel)
    {
        C_MatchingReq req = new C_MatchingReq();

        req.IsCancel = isCancel;

        NetworkManager.Instance.Send(req);
    }
}

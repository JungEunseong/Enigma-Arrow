using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : Singleton<LobbyManager>
{

    [SerializeField] GameObject _onMatchingPanel;
    [SerializeField] GameObject _userInfoPanel;
    [SerializeField] GameObject _firstUserInfo;
    [SerializeField] GameObject _secondUserInfo;
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

    public void OnMatching(S_MatchingRes res)
    {
        TMP_Text firstUserNickname = _firstUserInfo.GetComponentInChildren<TMP_Text>();
        firstUserNickname.text = res.Users[0].NickName;
        TMP_Text secondUserNickname = _secondUserInfo.GetComponentInChildren<TMP_Text>();
        secondUserNickname.text = res.Users[1].NickName;
    
        _userInfoPanel.SetActive(true);
    }

    public void OnIngameButtonOn()
    {
        SceneManager.LoadScene("IngameScene");
    }
}

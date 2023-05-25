using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] TMP_InputField nickName_IF;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnNicknameEnterButtonOn()
    {
        if(nickName_IF.text != "")
        {
            C_SetUserinfo setUserinfo= new C_SetUserinfo();
            NetworkManager.Instance.userInfo.NickName = nickName_IF.text;
            setUserinfo.Info = NetworkManager.Instance.userInfo;

            NetworkManager.Instance.Send(setUserinfo);

            SceneManager.LoadScene("LobbyScene");
        }
    }
}

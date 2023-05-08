using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

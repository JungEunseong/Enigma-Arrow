using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class OutComeUI : Singleton<OutComeUI> 
{
    [SerializeField] GameObject[] outcomePanels;

    private void Start()
    {
        for (int i = 0; i < outcomePanels.Length; i++)
        {
            outcomePanels[i].SetActive(false);
        }
    }

    public void ShowOutcomePanel(bool win)
    {
        GameObject panel = win == true ? outcomePanels[0] : outcomePanels[1];
        panel.SetActive(true);
        outcomePanels[2].SetActive(true);

        GameManager.Instance.isGameEnd = true;
    }
}

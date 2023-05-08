using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class OutComeUI : Singleton<OutComeUI> 
{
    [SerializeField] GameObject[] outcomePanels;

    public void ShowOutcomePanel(bool win)
    {
        GameObject panel = win == true ? outcomePanels[0] : outcomePanels[1];
    }
}

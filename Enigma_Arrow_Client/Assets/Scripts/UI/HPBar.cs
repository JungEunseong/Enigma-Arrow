using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider _slider;

    public void SetMaxHP(int hp)
    {
        _slider.maxValue= hp;
        _slider.value= hp;
    }

    public void SetHP(int hp)
    {
        _slider.value = hp;
    }
}

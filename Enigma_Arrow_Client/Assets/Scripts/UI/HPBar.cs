using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider _slider;
    public Gradient _gradient;
    public Image _fill;

    public void SetMaxHP(int hp)
    {
        _slider.maxValue= hp;
        _slider.value= hp;

        _fill.color = _gradient.Evaluate(1f);
    }

    public void SetHP(int hp)
    {
        _slider.value = hp;
        _fill.color = _gradient.Evaluate(_slider.normalizedValue);
    }
}

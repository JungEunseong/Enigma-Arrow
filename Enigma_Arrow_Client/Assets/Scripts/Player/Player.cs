using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _hp;
    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp= value;

            if(_hp <= 0)
            {
                GameOver();
            }
        }
    }

    public void Hit(int damage)
    {
        HP -= damage;   
    }


    private void GameOver()
    {
        Debug.Log("Á×À½");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : NetworkingObject
{
    [SerializeField] private int _hp;
    [SerializeField] PlayerMovement movement;
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

    public const int MaxHP = 100;

    public HPBar _hpBar;

    private void Start()
    {
        HP = MaxHP;
        _hpBar.SetMaxHP(MaxHP);
    }

    private void Update()
    {
        _hpBar.SetHP(HP);
        if (isMine)
        {
            movement.Move();
        }
    }

    public void Hit(int damage)
    {
        HP -= damage;  
        
    }


    private void GameOver()
    {
        Debug.Log("����");

        // ���� ���� �ƴ��� ó�� �Ű����� �ֱ�
        //GameManager.Instance.GameOver(PV.isMine ? true: false);
    }
}

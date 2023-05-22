using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : NetworkingObject
{
    [SerializeField] private int _hp;
    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerAttack attack;

    public bool IsTopPlayer { get => attack.isTopPlayer; set => attack.isTopPlayer = value; }

    [SerializeField] Canvas btnCanvas;
    [SerializeField] Canvas hpCanvas;

    [SerializeField] GameObject attackObj;
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
        movement = GetComponent<PlayerMovement>();
        HP = MaxHP;
        _hpBar.SetMaxHP(MaxHP);
        hpCanvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        _hpBar.SetHP(HP);
        if (isMine)
            movement.Move();

        if(!NetworkManager.Instance.isTestWithoutServer)
            SyncMove(destPos);

    }

    public void Hit(int damage)
    {
        HP -= damage;  
        
    }


    private void GameOver()
    {
        Debug.Log("죽음");

        // 본인 인지 아닌지 처리 매개변수 넣기
        //GameManager.Instance.GameOver(PV.isMine ? true: false);
    }
    public override void SyncMove(Vector3 pos)
    {
        destPos = pos;
        transform.position = Vector3.Lerp(transform.position, destPos, (1000 / 60 /movement.Speed)*Time.deltaTime);
    }

    public void RemotePlayerInit()
    {
        btnCanvas.gameObject.SetActive(false);
        attackObj.SetActive(false);
    }
}

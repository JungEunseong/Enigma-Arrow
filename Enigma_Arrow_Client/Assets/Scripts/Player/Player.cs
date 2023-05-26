using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Player : NetworkingObject
{
    [SerializeField] private int _hp;
    [SerializeField] GameObject model;
    [SerializeField] PlayerMovement movement;
    [SerializeField] public PlayerAttack attack;
    public Animator _anim;

    public bool IsTopPlayer { get => NetworkManager.Instance.isTopPosition; set => NetworkManager.Instance.isTopPosition = value; }

    [SerializeField] Canvas btnCanvas;
    [SerializeField] Canvas hpCanvas;
    [SerializeField] TMP_Text nickNameText;
    [SerializeField] Transform[] UI_Transform;
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

            if (NetworkManager.Instance.isTestWithoutServer)
            {
                if (_hp <= 0)
                {
                    GameOver();
                }
            }
        }
    }

    public const int MaxHP = 100;

    public HPBar _hpBar;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        
    }

    private void Start()
    {
        if (IsTopPlayer)
            model.transform.Rotate(0, 180, 0);

            movement = GetComponent<PlayerMovement>();
        HP = MaxHP;
        _hpBar.SetMaxHP(MaxHP);
        hpCanvas.worldCamera = Camera.main;

        if (attack.isTopPlayer)
        {
            hpCanvas.transform.LookAt(Camera.main.transform);
            _hpBar.transform.localEulerAngles = new Vector3(0, 180, 0);
            nickNameText.transform.localEulerAngles = new Vector3(0, 180, 0);
        }

        hpCanvas.transform.position = (IsTopPlayer) ? UI_Transform[0].position : UI_Transform[1].position;

        nickNameText.text = (isMine) ? NetworkManager.Instance.userInfo.NickName : NetworkManager.Instance.enemyInfo.NickName;
        nickNameText.color = (isMine) ? Color.black : Color.red;
    }

    private void Update()
    {
        if (GameManager.Instance.isGameEnd) return;

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
        transform.position = Vector3.Lerp(transform.position, destPos, (movement.Speed)*Time.deltaTime);

        _anim.SetBool("Walk",Mathf.Abs(transform.position.x - destPos.x) >= 0.1f);

    }

    public void RemotePlayerInit()
    {
        btnCanvas.gameObject.SetActive(false);
        attackObj.SetActive(false);
    }
}

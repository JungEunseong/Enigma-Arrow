using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.ExceptionServices;
using Google.Protobuf.Protocol;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] private float _speed;
    Rigidbody rigid;

    public float Speed { get { return _speed; } }
    Vector3 _prevDir = Vector3.zero;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame


    /// <summary>
    /// ������ �Լ�
    /// </summary>
    public void Move()
    {
        Vector3 _dir = Vector3.zero;        //�̵� ���� �ʱ�ȭ

        float h = Input.GetAxis("Horizontal");
        _dir.x = h;

        //_dir.x = Input.acceleration.x;

        if (_dir.sqrMagnitude > 1)
            _dir.Normalize();
            
        if (NetworkManager.Instance.isTestWithoutServer)
            rigid.velocity = new Vector2(_dir.x * _speed, 0);
        else
        {

            if (_player.attack.IsAttack)
            {
                C_MoveReq req = new C_MoveReq();

                req.InputDir = new Vec();
                req.InputDir.X = 0;
                req.InputDir.Y = 0;
                req.InputDir.Z = 0;

                NetworkManager.Instance.Send(req);

            }
            else
            {

                if (_prevDir != _dir)
                {

                    C_MoveReq req = new C_MoveReq();
                    req.InputDir = new Vec();

                    req.InputDir.X = _dir.x;
                    req.InputDir.Y = _dir.y;
                    req.InputDir.Z = _dir.z;

                    NetworkManager.Instance.Send(req);

                    _prevDir = _dir;
                }
            }
        }

    }


}
